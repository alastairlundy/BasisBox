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
using AlastairLundy.Extensions.IO.Directories.Extensions;
using AlastairLundy.Extensions.IO.Files;

using BasisBox.Cli.Helpers;
using BasisBox.Cli.Localizations;
using BasisBox.Cli.Tools.Del.Helpers;

using CliUtilsLib;

using Spectre.Console;
using Spectre.Console.Cli;

namespace BasisBox.Cli.Tools.Del.Commands;

public class DeleteManyCommand : Command<DeleteManyCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<File(s) Or Directories To Be Deleted>")]
        public string[]? FilesOrDirectoriesToBeDeleted { get; init; }
        
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

        int fileResult = FileArgumentHelpers.HandleFileArgument(settings.FilesOrDirectoriesToBeDeleted, exceptionFormats);

        if (fileResult == -1)
        {
            return -1;
        }

        if (settings.FilesOrDirectoriesToBeDeleted.Contains("/"))
        {
            AnsiConsole.WriteException(new ArgumentException(Resources.Exceptions_InvalidSlashCommand), exceptionFormats);
            return -1;
        }

        FileRemover fileRemover = new FileRemover();
        DirectoryRemover directoryRemover = new DirectoryRemover();

        fileRemover.FileDeleted += DirectoryRemoverOnFileDeleted;
        directoryRemover.FileDeleted += DirectoryRemoverOnFileDeleted;
        directoryRemover.DirectoryDeleted += DirectoryRemoverOnDirectoryDeleted;

        void DirectoryRemoverOnDirectoryDeleted(object? sender, string e)
        {
            if (settings.Verbose)
            {
                AnsiConsole.WriteLine(e);
            }
        }

        void DirectoryRemoverOnFileDeleted(object? sender, string e)
        {
            if (settings.Verbose)
            {
                AnsiConsole.WriteLine(e);
            }
        }
        
        try
        {
            foreach (string fileOrDirectory in settings.FilesOrDirectoriesToBeDeleted!)
            {
                if (fileOrDirectory.Equals("*"))
                {
                    if (settings.Interactive && !settings.Force)
                    {
                        RecursiveDirectoryExplorer recursiveDirectoryExplorer = new RecursiveDirectoryExplorer();
                        
                        (IEnumerable<string> files, IEnumerable<string> directories, IEnumerable<string> emptyDirectories) 
                            = recursiveDirectoryExplorer.GetRecursiveDirectoryContents(Environment.CurrentDirectory, settings.DeleteEmptyDirectory);

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
                        directoryRemover.DeleteDirectoryRecursively(Environment.CurrentDirectory, settings.DeleteEmptyDirectory);
                    }
                }
                else if (Directory.Exists(fileOrDirectory))
                {
                    if (fileOrDirectory.IsDirectoryEmpty())
                    {
                        if (settings.DeleteEmptyDirectory)
                        {
                            bool deleteDirectory = true;
                            if (settings.Interactive && !settings.Force)
                            {
                                deleteDirectory = InteractiveInputHelper.DeleteDirectory(fileOrDirectory);
                            }

                            if (deleteDirectory)
                            {
                                Directory.Delete(fileOrDirectory);
                                
                                if (settings.Verbose)
                                {
                                    AnsiConsole.WriteLine(Resources.Exceptions_DirectoryNotEmpty.Replace("{x}", fileOrDirectory));   
                                }
                            }
                        }
                    }
                    else
                    {
                        if (settings.RecursivelyDeleteDirectories)
                        {
                            if (settings.Interactive && !settings.Force)
                            {
                                RecursiveDirectoryExplorer recursiveDirectoryExplorer =
                                    new RecursiveDirectoryExplorer();
                                
                               (IEnumerable<string> files, IEnumerable<string> directories, IEnumerable<string> emptyDirectories) 
                                   = recursiveDirectoryExplorer.GetRecursiveDirectoryContents(fileOrDirectory, settings.DeleteEmptyDirectory);

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
                                directoryRemover.DeleteDirectoryRecursively(fileOrDirectory, settings.DeleteEmptyDirectory);
                            }
                        }
                        else
                        {
                            bool deleteItem = true;

                            if (settings.Interactive && !settings.Force)
                            {
                                deleteItem = InteractiveInputHelper.DeleteFile(fileOrDirectory);
                            }

                            if (deleteItem)
                            {
                               directoryRemover.DeleteDirectory(fileOrDirectory, settings.DeleteEmptyDirectory, false); 
                            }
                        }
                    }
                }
                else if (File.Exists(fileOrDirectory))
                {
                    if (settings.Interactive)
                    {
                        bool deleteFile = InteractiveInputHelper.DeleteFile(fileOrDirectory);

                        if (deleteFile)
                        {
                            fileRemover.DeleteFile(fileOrDirectory);
                        }
                    }
                    else
                    {
                        fileRemover.DeleteFile(fileOrDirectory);
                    }
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