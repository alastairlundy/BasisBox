/*
    BasisBox - Parrot
    Copyright (C) 2024 Alastair Lundy

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Lesser General Public License as published by
    the Free Software Foundation, version 3 of the License.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Lesser General Public License for more details.

    You should have received a copy of the GNU Lesser General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

using AlastairLundy.Extensions.System.Strings.EscapeCharacters;

using BasisBox.Cli.Localizations;

using Spectre.Console;
using Spectre.Console.Cli;

namespace BasisBox.Cli.Tools.Parrot.Commands;

public class EchoCommand : Command<EchoCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<Lines To Print>")]
        public IEnumerable<string>? LinesToPrint { get; init; }
            
        [CommandOption("-n")]
        [DefaultValue(false)]
        public bool DisableTrailingNewLine { get; init; }
        
        [CommandOption("-e")]
        [DefaultValue(false)]
        public bool EnableParsingOfBackslashEscapeChars { get; init; }
        
        [CommandOption("-E")]
        [DefaultValue(true)]
        public bool DisableInterpretationOfBackslashEscapeChars { get; init; }

        [CommandOption("--output")]
        public string? OutputFile { get; init; }
    }


    public override int Execute(CommandContext context, Settings settings)
    {
        string[] linesToPrint;
        
        if (settings.LinesToPrint == null)
        {
            AnsiConsole.WriteException(new ArgumentException());
            return -1;
            //  if(context.Remaining.Parsed)
        }
        else
        {
            linesToPrint = settings.LinesToPrint.ToArray();
        }
        
        if (settings.DisableInterpretationOfBackslashEscapeChars &&
            settings.EnableParsingOfBackslashEscapeChars == false)
        {
             for(int index = 0; index < settings.LinesToPrint!.Count(); index++)
             {
                 linesToPrint[index] = linesToPrint[index].RemoveEscapeCharacters();
             }
        }

        if(settings.OutputFile == null)
        {
            if (settings.DisableTrailingNewLine)
            {
                foreach (string line in settings.LinesToPrint!)
                {
                    AnsiConsole.Write(line);
                }
                
                return 0;
            }
            else
            {
                foreach (string line in settings.LinesToPrint!)
                {
                    AnsiConsole.WriteLine(line);
                }

                return 0;
            }
        }
        else
        {
            try
            {
                if (File.Exists(settings.OutputFile))
                {
                  if(settings.OverrideIfOutputExists == null)
                  {
                        settings.OverrideIfOutputExists = false;
                  }
                  else if(settings.OverrideIfOutputExists == false)
                  {

                  }

                }

                if(settings.DisableTrailingNewLine)
                {
                    File.WriteAllText(settings.OutputFile, settings.LinesToPrint!.ToString());
                }
                else
                {
                    File.WriteAllLines(settings.OutputFile, settings.LinesToPrint!);
                }

                AnsiConsole.WriteLine($"{Resources.File_Saved_Failure}: {settings.OutputFile}");
                return 0;
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteLine($"{Resources.File_Saved_Failure}: {settings.OutputFile}");
                AnsiConsole.WriteException(ex);
                return -1;
            }
        }
    }
}