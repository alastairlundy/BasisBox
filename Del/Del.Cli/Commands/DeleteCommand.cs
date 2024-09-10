/*
    BasisBox - Del
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

using AlastairLundy.Extensions.Collections.IEnumerables;

using CliUtilsLib;

using Del.Cli.Helpers;
using Del.Cli.Localizations;

using Del.Library;

using Spectre.Console;
using Spectre.Console.Cli;

namespace Del.Cli.Commands;

public class DeleteCommand : Command<DeleteCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<File or Directory To Be Deleted>")]
        public string? FileOrDirectoryToBeDeleted { get; init; }
        
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
        ExceptionFormats exceptionFormats;

        if (settings.Verbose)
        {
            exceptionFormats = ExceptionFormats.Default;
        }
        else
        {
           exceptionFormats = ExceptionFormats.NoStackTrace;
        }
        
        if (settings.FileOrDirectoryToBeDeleted == null)
        {
            AnsiConsole.WriteException(new ArgumentNullException(nameof(settings.FileOrDirectoryToBeDeleted), Resources.Exceptions_NoArgumentsProvided), exceptionFormats);
            return -1;
        }

        if (settings.FileOrDirectoryToBeDeleted.Equals("/"))
        {
            AnsiConsole.WriteException(new ArgumentException(Resources.Exceptions_InvalidSlashArgument), exceptionFormats);
            return -1;
        }
        
        try
        {
            if (FileFinder.IsAFile(settings.FileOrDirectoryToBeDeleted))
            {
                if (File.Exists(settings.FileOrDirectoryToBeDeleted))
                {
                    FileRemover fileRemover = new FileRemover();
                    fileRemover.FileDeleted += FileRemoverOnFileDeleted;

                    void FileRemoverOnFileDeleted(object? sender, string e)
                    {
                        if (settings.Verbose)
                        {
                            AnsiConsole.WriteLine(e);
                        }
                    }

                    if (settings.Interactive && !settings.Force)
                    {
                        bool deleteFile = InteractiveInputHelper.DeleteFile(settings.FileOrDirectoryToBeDeleted);

                        if (deleteFile)
                        {
                            fileRemover.DeleteFile(settings.FileOrDirectoryToBeDeleted);
                        }
                    }
                }
            }
            else
            {
                if (settings.FileOrDirectoryToBeDeleted.Equals("*"))
                {
                    DirectoryRemover directoryRemover = new DirectoryRemover();
                    directoryRemover.DirectoryDeleted += RemoverOnDeleted;
                    directoryRemover.FileDeleted += RemoverOnDeleted;

                    FileRemover fileRemover = new FileRemover();
                    fileRemover.FileDeleted += RemoverOnDeleted;

                    void RemoverOnDeleted(object? sender, string e)
                    {
                        if (settings.Verbose)
                        {
                            AnsiConsole.WriteLine(e);
                        }
                    }

                    if (settings.Interactive && !settings.Force)
                    {
                        (IEnumerable<string> files, IEnumerable<string> directories, IEnumerable<string> emptyDirectories) 
                            = RecursiveDirectoryExplorer.GetDirectoryContents(Environment.CurrentDirectory, settings.DeleteEmptyDirectory);

                        List<string> filesToBeDeleted = InteractiveRecursiveDeletionHelper.GetFilesToBeDeleted(files).ToList();
                        List<string> directoriesToBeDeleted = InteractiveRecursiveDeletionHelper.GetDirectoriesToBeDeleted(directories).ToList();

                        string[] emptyDirectoryEnumerable = emptyDirectories as string[] ?? emptyDirectories.ToArray();
                        if (emptyDirectoryEnumerable.Any())
                        {
                            directoriesToBeDeleted = directoriesToBeDeleted.Combine(InteractiveRecursiveDeletionHelper.GetDirectoriesToBeDeleted(emptyDirectoryEnumerable)).ToList();
                        }
                        
                        fileRemover.DeleteFiles(filesToBeDeleted);
                        directoryRemover.DeleteDirectories(directoriesToBeDeleted, settings.DeleteEmptyDirectory, false);
                    }
                    else
                    {
                        directoryRemover.DeleteRecursively(Environment.CurrentDirectory, settings.DeleteEmptyDirectory);
                    }

                    return 0;
                }
                if (Directory.Exists(settings.FileOrDirectoryToBeDeleted))
                {
                    DirectoryRemover directoryRemover = new DirectoryRemover();
                    directoryRemover.FileDeleted += DirectoryRemoverOnDeleted;
                    directoryRemover.DirectoryDeleted += DirectoryRemoverOnDeleted;

                    void DirectoryRemoverOnDeleted(object? sender, string e)
                    {
                        if (settings.Verbose)
                        {
                            AnsiConsole.WriteLine(e);
                        }
                    }

                    if (settings.Interactive && !settings.Force)
                    {
                        if (settings.RecursivelyDeleteDirectories)
                        {
                            (IEnumerable<string> files, IEnumerable<string> directories, IEnumerable<string> emptyDirectories) 
                                = RecursiveDirectoryExplorer.GetDirectoryContents(settings.FileOrDirectoryToBeDeleted, settings.DeleteEmptyDirectory);

                            List<string> filesToBeDeleted = InteractiveRecursiveDeletionHelper.GetFilesToBeDeleted(files).ToList();
                            List<string> directoriesToBeDeleted = InteractiveRecursiveDeletionHelper.GetDirectoriesToBeDeleted(directories).ToList();

                            string[] emptyDirectoryEnumerable = emptyDirectories as string[] ?? emptyDirectories.ToArray();
                            if (emptyDirectoryEnumerable.Any())
                            {
                                directoriesToBeDeleted = directoriesToBeDeleted.Combine(InteractiveRecursiveDeletionHelper.GetDirectoriesToBeDeleted(emptyDirectoryEnumerable)).ToList();
                            }

                            FileRemover fileRemover = new FileRemover();
                            fileRemover.FileDeleted += FileRemoverOnFileDeleted;

                            void FileRemoverOnFileDeleted(object? sender, string e)
                            {
                                if (settings.Verbose)
                                {
                                    AnsiConsole.WriteLine(e);
                                }
                            }

                            directoryRemover.DeleteDirectories(directoriesToBeDeleted, settings.DeleteEmptyDirectory, false);
                            fileRemover.DeleteFiles(filesToBeDeleted);
                            
                        }
                        else
                        {
                            bool deleteDirectory = InteractiveInputHelper.DeleteDirectory(settings.FileOrDirectoryToBeDeleted);

                            if (deleteDirectory)
                            {
                                directoryRemover.DeleteDirectory(settings.FileOrDirectoryToBeDeleted, settings.DeleteEmptyDirectory, false);
                            }
                        }
                    }
                    else
                    {
                        if (settings.RecursivelyDeleteDirectories)
                        {
                            directoryRemover.DeleteRecursively(settings.FileOrDirectoryToBeDeleted, settings.DeleteEmptyDirectory);
                        }
                        else
                        {
                            directoryRemover.DeleteDirectory(settings.FileOrDirectoryToBeDeleted, settings.DeleteEmptyDirectory, false);
                        }
                    }
                    
                    return 0;
                }
                else
                {
                    throw new DirectoryNotFoundException(Resources.Exceptions_DirectoryNotFound.Replace("{x}", settings.FileOrDirectoryToBeDeleted));
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