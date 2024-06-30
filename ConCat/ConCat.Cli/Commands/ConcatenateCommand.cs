using System;

using ConCat.Cli.Localizations;
using ConCat.Cli.Settings;

using Spectre.Console;
using Spectre.Console.Cli;

namespace ConCat.Cli.Commands;

public class ConcatenateCommand : Command<ConcatenateCommand.Settings>
{
    public class  Settings : BasicConCatSettings
    {
        
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        if (settings.Files == null || settings.Files.Length == 0)
        {
            AnsiConsole.WriteException(new NullReferenceException(Resources.Exceptions_NoFileProvided));
            return -1;
        }
        
        
    }
}