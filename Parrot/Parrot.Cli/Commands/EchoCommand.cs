using System;
using System.ComponentModel;
using System.IO;

using Parrot.Cli.Helpers;
using Parrot.Cli.Localizations;
using Parrot.Library;

using Spectre.Console;
using Spectre.Console.Cli;

namespace Parrot.Cli.Commands;

public class EchoCommand : Command<EchoCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<Lines To Print>")]
        public string[]? LinesToPrint { get; init; }
            
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
        if (settings.LinesToPrint == null)
        {
            

          //  if(context.Remaining.Parsed)
        }
        
        if (settings.DisableInterpretationOfBackslashEscapeChars &&
            settings.EnableParsingOfBackslashEscapeChars == false)
        {
             for(int index = 0; index < settings.LinesToPrint!.Length; index++)
             {
                settings.LinesToPrint[index] = EscapeCharacterFormatter.RemoveEscapeChars(settings.LinesToPrint[index]);
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

                AnsiConsole.WriteLine($"{Resources.File_Save_Success}: {settings.OutputFile}");
                return 0;
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteLine($"{Resources.File_Save_Exception}: {settings.OutputFile}");
                AnsiConsole.WriteException(ex);
                return -1;
            }
        }
    }
}