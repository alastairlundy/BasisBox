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

using AlastairLundy.Extensions.System.VersionExtensions;
using AlastairLundy.Extensions.System.VersionExtensions.Enums;

using AlastairLundy.Extensions.System;

using McMaster.Extensions.CommandLineUtils;
using System.Reflection;

using AlastairLundy.Extensions.System.AssemblyExtensions;
using average.localizations;
using average.Helpers;

namespace average;

internal class Program
{
    static int Main(string[] args)
    {
        CommandLineApplication app = new CommandLineApplication();

        var appVersion = Assembly.GetExecutingAssembly().GetProjectVersion();

        var help = app.HelpOption("-h|--help");
        var license = app.Option("-l|--license", Resources.Command_License_Description, CommandOptionType.NoValue);
        var version = app.Option("-v|--version", "", CommandOptionType.NoValue);

        app.Command("mean", meanCommand =>
        {
            var geometricFlag = meanCommand.Option("--geometric", Resources.Command_GeometricMean_Description, CommandOptionType.NoValue);
            var arithmeticFlag = meanCommand.Option("-arithmetic", Resources.Command_ArithmeticMean_Description, CommandOptionType.NoValue);

            var decimalRounding = meanCommand.Option("-dp|--decimal-places", Resources.Rounding_DecimalPlaces_Description, CommandOptionType.SingleValue);

            var numbers = meanCommand.Argument("<numbers>", Resources.Numbers_Description, true);


            if(geometricFlag.HasValue() && arithmeticFlag.HasValue())
            {

            }
            else if (geometricFlag.HasValue() && !arithmeticFlag.HasValue())
            {

            }
            else if (arithmeticFlag.HasValue() && !geometricFlag.HasValue())
            {

            }



        });

        app.Command("median", medianCommand =>
        {

        });

        app.Command("mode", modeCommand =>
        {
            var modeHelp = modeCommand.HelpOption("-h|--help");

            modeCommand.Description = Resources.Command_Mode_About;

            var prettyMode = modeCommand.Option("--pretty", Resources.Output_Pretty_Description, CommandOptionType.NoValue);

            var numbers = modeCommand.Argument("<numbers>", Resources.Numbers_Description, true);


            modeCommand.OnExecute(() =>
            {

                decimal[] modes;

                if (numbers.Values.Count > 1)
                {
                    modes = ModeHelper.GetModes(numbers.Values.ToArray()!);
                }
                else
                {
                    if (numbers.Values.Count == 1)
                    {
                        string[] array = new string[] { numbers.Value! };

                        modes = ModeHelper.GetModes(array);
                    }


                }

                if(prettyMode.HasValue())
                {
                    if(modes != null)
                    {
                        ModeHelper.PrintPrettyMode(modes);
                    }
                }
                else
                {
                   if(modes != null)
                    {
                        ConsoleHelper.PrintUnformattedDecimalArray(modes);
                    }
                }

                if (modeHelp.HasValue())
                {
                    modeCommand.ShowHelp();
                }
            });
        });

        app.Command("midrange", midRangeCommand =>
        {
            var rounding = midRangeCommand.Option("-r|--round", "", CommandOptionType.SingleValue);

        });

        app.Command("interquartilemean", iqrMean =>
        {

        });


        app.OnExecute(() =>
        {
            if(version.HasValue()) 
            {
                ConsoleHelper.PrintUnformattedVersion();
            }

            if(help.HasValue())
            {
                app.ShowHelp();
            }
            if (license.HasValue())
            {
                ConsoleHelper.PrintUnformattedLicense();
            }
        });

        return app.Execute(args);
    }
}
