/*

   Copyright 2024 Alastair Lundy

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
 
 */

using System;
using System.Collections.Generic;
using System.IO;

using AlastairLundy.Extensions.System.DecimalArrayExtensions;
using AlastairLundy.Extensions.System.Maths;

using Pow.Cli.Localizations;

using Spectre.Console;
using Spectre.Console.Cli;

namespace Pow.Cli.Commands;

public class PowerCommand : Command<PowerCommand.Settings>
{
    public class Settings : SharedSettings
    {
        [CommandOption("-e|--exponent")]
        public decimal? Exponent { get; init; }
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        if (settings.Inputs == null || settings.Inputs.Length == 0 || settings.Exponent == null)
        {
            AnsiConsole.WriteException(new NullReferenceException());
            return -1;
        }

        List<decimal> results = new List<decimal>();

        foreach (decimal input in settings.Inputs)
        {
            decimal power = Power.ToDecimal(input, (decimal)settings.Exponent);
            
            results.Add(power);
        }

        if (settings.OutputFile != null)
        {
            try
            {
                if (!File.Exists(settings.OutputFile))
                {
                    File.WriteAllLines(settings.OutputFile, results.ToArray().ToStringArray());
                    AnsiConsole.WriteLine($"{Resources.FileSaved_Success} {settings.OutputFile}");
                    return 0;
                }

                throw new ArgumentException(Resources.FileSaved_AlreadyExists);
            }
            catch(Exception exception)
            {
                AnsiConsole.WriteException(exception);
                return -1;
            }
        }
        // ReSharper disable once RedundantIfElseBlock
        else
        {
            foreach (string result in results.ToArray().ToStringArray())
            {
                AnsiConsole.WriteLine(result);
            }

            return 0;
        }
    }
}
