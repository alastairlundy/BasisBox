using System.ComponentModel;
using NewDir.Cli.Localizations;
using NewDir.Library;
using Spectre.Console;
using Spectre.Console.Cli;

namespace NewDir.Cli.Commands;

public class NewDirCommand : Command<NewDirCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<directory_name>")]
        public string? DirectoryName { get; init; }
        
        [CommandOption("-p|--parents")]
        [DefaultValue(false)]
        public bool CreateParentDirectories { get; init; }
        
        [CommandOption("-m|--mode")]
        public string? Mode { get; init; }
        
        [CommandOption("--debug|--debugging")]
        [DefaultValue(false)]
        public bool UseDebugging { get; init; }
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        if (settings.DirectoryName == null)
        {
            if (settings.UseDebugging)
            {
                AnsiConsole.WriteException(new NullReferenceException(Resources.Exceptions_DirectoryNotSpecified));
            }
            else
            {
                AnsiConsole.WriteException(new NullReferenceException(Resources.Exceptions_DirectoryNotSpecified), ExceptionFormats.NoStackTrace);
            }
            return -1;
        }

        try
        {
            UnixFileMode fileMode;

            if (settings.Mode == null)
            {
                fileMode = UnixFileMode.UserWrite & UnixFileMode.UserRead;
            }
            else
            {
                fileMode = UnixFilePermissionTranslator.Parse(settings.Mode);
            }
            
            NewDirectory.Create(settings.DirectoryName, fileMode, settings.ParentDirectories);
        }
        
        throw new NotImplementedException();
    }
}