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
using Del.Library.Extensions;

using Spectre.Console;
using Spectre.Console.Cli;

namespace Del.Cli.Commands;

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
        
        if (settings.FilesOrDirectoriesToBeDeleted == null)
        {
            AnsiConsole.WriteException(new ArgumentNullException(nameof(settings.FilesOrDirectoriesToBeDeleted),  Resources.Exceptions_NoArgumentsProvided), exceptionFormats);
            return -1;
        }
        
        if (settings.FilesOrDirectoriesToBeDeleted.Contains("/"))
        {
            AnsiConsole.WriteException(new ArgumentException(Resources.Exceptions_InvalidSlashArgument), exceptionFormats);
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
                                    AnsiConsole.WriteLine(Resources.EmptyDirectory_Deleted.Replace("{x}", fileOrDirectory));   
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
                               (IEnumerable<string> files, IEnumerable<string> directories, IEnumerable<string> emptyDirectories) = RecursiveDirectoryExplorer.GetDirectoryContents(fileOrDirectory, settings.DeleteEmptyDirectory);

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
                                directoryRemover.DeleteRecursively(fileOrDirectory, settings.DeleteEmptyDirectory);
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