using System;
using System.ComponentModel;
using System.IO;
using CliUtilsLib;
using Del.Cli.Localizations;
using Del.Library;
using Del.Library.Extensions;

using Spectre.Console;
using Spectre.Console.Cli;

namespace Del.Cli.Commands;

public class DeleteCommand : Command<DeleteCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(1, "<File Or Directory To Be Deleted>")]
        public string[]? FileOrDirectoryToBeDeleted { get; init; }
        
        [CommandOption("-r|--recursive")]
        [DefaultValue(false)]
        public bool RecursivelyDeleteDirectories { get; init; }
        
        [CommandOption("-f|--force")]
        [DefaultValue(false)]
        public bool Force { get; init; }
        
        [CommandOption("-i|--interactive")]
        [DefaultValue(false)]
        public bool Interactive { get; init; }
        
        [CommandOption("-v|--verbose")]
        [DefaultValue(false)]
        public bool Verbose { get; init; }
        
        [CommandOption("-d|--directory")]
        [DefaultValue(false)]
        public bool DeleteEmptyDirectory { get; init; }
    }


    public override int Execute(CommandContext context, Settings settings)
    {
        if (settings.FileOrDirectoryToBeDeleted == null)
        {
            
        }

        ExceptionFormats exceptionFormats;

        if (settings.Verbose)
        {
            exceptionFormats = ExceptionFormats.Default;
        }
        else
        {
            exceptionFormats = ExceptionFormats.NoStackTrace;
        }

        try
        {
            foreach (string fileOrDirectory in settings.FileOrDirectoryToBeDeleted!)
            {
                if (Directory.Exists(fileOrDirectory))
                {
                    if (fileOrDirectory.IsDirectoryEmpty())
                    {
                        if (settings.DeleteEmptyDirectory)
                        {
                            Directory.Delete(fileOrDirectory);
                        }
                    }
                    else
                    {
                        if (settings.RecursivelyDeleteDirectories)
                        {
                            DirectoryEliminator.DeleteRecursively(fileOrDirectory, settings.DeleteEmptyDirectory);
                        }
                        else
                        {
                            Directory.Delete(fileOrDirectory);
                        }
                    }
                }
                else if (File.Exists(fileOrDirectory))
                {
                    
                }
                else if (!Directory.Exists(fileOrDirectory) && !File.Exists(fileOrDirectory))
                {
                    if (FileFinder.IsAFile(fileOrDirectory))
                    {
                        throw new FileNotFoundException(Resources.Exceptions_FileNotFound.Replace("{x}", fileOrDirectory), fileOrDirectory);
                    }
                    else
                    {
                        throw new DirectoryNotFoundException(Resources.Exceptions_DirectoryNotFound.Replace("{x}", fileOrDirectory));
                    }
                }
            }
            
            return 0;
        }
        catch(Exception exception)
        {
            AnsiConsole.WriteException(exception, exceptionFormats);
            return -1;
        }
    }
}