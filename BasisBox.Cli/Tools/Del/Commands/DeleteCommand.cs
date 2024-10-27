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

using AlastairLundy.Extensions.IO.Directories;
using AlastairLundy.Extensions.IO.Directories.Abstractions;
using AlastairLundy.Extensions.IO.Files;
using AlastairLundy.Extensions.IO.Files.Removal;
using BasisBox.Cli.Helpers;
using BasisBox.Cli.Localizations;
using BasisBox.Cli.Tools.Del.Helpers;

using CliUtilsLib;

using Spectre.Console;
using Spectre.Console.Cli;
using FileFinder = AlastairLundy.Extensions.IO.Files.FileFinder;

namespace BasisBox.Cli.Tools.Del.Commands;

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

        int fileResult = FileArgumentHelpers.HandleFileArgument(settings.FileOrDirectoryToBeDeleted, exceptionFormats);

        if (fileResult == -1)
        {
            return -1;
        }

        if (settings.FileOrDirectoryToBeDeleted!.Equals("/"))
        {
            AnsiConsole.WriteException(new ArgumentException(Resources.Exceptions_InvalidSlashCommand), exceptionFormats);
            return -1;
        }
        
        try
        {
            FileFinder fileFinder = new FileFinder();
            if (fileFinder.IsAFile(settings.FileOrDirectoryToBeDeleted))
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
                        IRecursiveDirectoryExplorer recursiveDirectoryExplorer = new RecursiveDirectoryExplorer();
                        
                        (IEnumerable<string> files, IEnumerable<string> directories, IEnumerable<string> emptyDirectories) 
                            = recursiveDirectoryExplorer.GetRecursiveDirectoryContents(Environment.CurrentDirectory, settings.DeleteEmptyDirectory);

                        List<string> filesToBeDeleted = InteractiveRecursiveDeletionHelper.GetFilesToBeDeleted(files).ToList();
                        List<string> directoriesToBeDeleted = InteractiveRecursiveDeletionHelper.GetDirectoriesToBeDeleted(directories).ToList();
                        
                        fileRemover.DeleteFiles(filesToBeDeleted);
                        directoryRemover.DeleteDirectories(directoriesToBeDeleted, settings.DeleteEmptyDirectory, false);
                    }
                    else
                    {
                        directoryRemover.DeleteDirectoryRecursively(Environment.CurrentDirectory, settings.DeleteEmptyDirectory);
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
                            IRecursiveDirectoryExplorer recursiveDirectoryExplorer = new RecursiveDirectoryExplorer();
                            
                            (IEnumerable<string> files, IEnumerable<string> directories, IEnumerable<string> emptyDirectories) 
                                = recursiveDirectoryExplorer.GetRecursiveDirectoryContents(settings.FileOrDirectoryToBeDeleted, settings.DeleteEmptyDirectory);

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
                            directoryRemover.DeleteDirectoryRecursively(settings.FileOrDirectoryToBeDeleted, settings.DeleteEmptyDirectory);
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