using System;
using System.ComponentModel;
using System.IO;
using System.Linq;

using ConCat.Cli.Helpers;
using ConCat.Cli.Localizations;

using Spectre.Console;
using Spectre.Console.Cli;

namespace ConCat.Cli.Commands;

public class ConcatenateCommand : Command<ConcatenateCommand.Settings>
{
    public class Settings : CommandSettings{

        [CommandArgument(0, "<File(s)>")]
        public string[]? Files { get; init; }

        [CommandOption("-n")]
        [DefaultValue(false)]
        public bool AppendLineNumbers { get; init; }
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        if(settings.Files == null || settings.Files.Length == 0)
        {
            AnsiConsole.WriteException(new NullReferenceException(Resources.Exceptions_NoFileProvided));
            return -1;
        }

        if(settings.Files.Length == 1)
        {
            if (settings.Files[0].StartsWith('>'))
            {
                string[] strings = AnsiConsole.Prompt(new TextPrompt<string[]>(Resources.Command_NewFile_Prompt).AllowEmpty().)
            }
            else
            {
                try
                {
                    ConsoleHelper.PrintLines(File.ReadAllLines(settings.Files[0]), settings.AppendLineNumbers!);
                    return 0;
                }
                catch(Exception exception) 
                {
                    AnsiConsole.WriteException(exception);
                    return -1;
                }
            }
        }

        if (context.Arguments.Contains(">>"))
        {

        }
        else if(context.Arguments.Contains(">"))
        {

        }




        
        
        throw new NotImplementedException();
    }
}