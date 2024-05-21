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

using Caesar.Library;

using Spectre.Console;
using Spectre.Console.Cli;

namespace Caesar.Commands;

public class EncodeCommand : Command<EncodeCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<Words>")]
        public string[]? Words { get; init; }
        
        [CommandOption("-o|--output:<file>")]
        public string? OutputFile { get; init; }
            
        [CommandOption("-s|--shift:<number_to_shift_by>")]
        public int? ShiftAmount { get; init; }
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        int shift = ConsoleHelper.ShiftHandler(settings.ShiftAmount);

        if (settings.Words == null)
        {
            AnsiConsole.WriteException(new NullReferenceException());
            return -1;
        }
        
        CaesarCipher caesarCipher = new CaesarCipher();
        
        string[] newValues = caesarCipher.Encode(settings.Words!, shift);

        if (settings.OutputFile != null)
        {
            ConsoleHelper.SaveResultsToFile(settings.OutputFile, newValues);
            return 0;
        }
        
        ConsoleHelper.PrintResults(newValues);
        return 0;
    }
}