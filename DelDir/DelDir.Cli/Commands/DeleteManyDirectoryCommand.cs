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
using System.ComponentModel;
using System.IO;
using System.Linq;

using Del.Library;

using DelDir.Cli.Localizations;

using Spectre.Console;
using Spectre.Console.Cli;

namespace DelDir.Cli.Commands;

public class DeleteManyDirectoriesCommand : Command<DeleteManyDirectoriesCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<Directories>")]
        public string[]? DirectoriesToBeDeleted { get; init; }
        
        [CommandOption("-v|--verbose")]
        [DefaultValue(false)]
        public bool Verbose { get; init; }
        
        [CommandOption("-p")]
        [DefaultValue(false)]
        public bool RemoveEmptyParentDirectories { get; init; }
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

        if (settings.DirectoriesToBeDeleted == null)
        {
            AnsiConsole.WriteException(new ArgumentNullException(Resources.Exception_NoArgumentsProvided));
            return -1;
        }
        
        try
        {
            foreach (string directory in settings.DirectoriesToBeDeleted)
            {
                if (directory.Equals("/"))
                {
                    throw new ArgumentException(Resources.Exception_InvalidSlashArgument, directory);
                }

                if (directory.Equals("*"))
                {
                    bool allowRecursiveEmptyDirectoryDeletion = false;

                    foreach (string subDirectory in Directory.GetDirectories(directory))
                    {
                        if (Directory.GetFiles(subDirectory).Length == 0)
                        {
                            allowRecursiveEmptyDirectoryDeletion = true;
                        }
                        else
                        {
                            allowRecursiveEmptyDirectoryDeletion = false;
                        }
                    }

                    if (allowRecursiveEmptyDirectoryDeletion)
                    {
                        DirectoryRemover directoryRemover = new DirectoryRemover();
                        directoryRemover.DirectoryDeleted += OnDeleted;
                        directoryRemover.FileDeleted += OnDeleted;

                        void OnDeleted(object? sender, string e)
                        {
                            if (settings.Verbose)
                            {
                                AnsiConsole.WriteLine(e);
                            }
                        }

                        directoryRemover.DeleteRecursively(directory, true);

                        if (settings.RemoveEmptyParentDirectories)
                        {
                            directoryRemover.DeleteParentDirectory(directory, settings.RemoveEmptyParentDirectories);
                        }
                    }
                    else
                    {
                        throw new ArgumentException(Resources.Exception_NonEmptyDirectory.Replace("{x}", directory));
                    }
                }

                if (Directory.Exists(directory))
                {
                    if (!Directory.GetDirectories(directory).Any() &&
                        !Directory.GetFiles(directory).Any())
                    {
                        DirectoryRemover directoryRemover = new DirectoryRemover();
                        directoryRemover.DirectoryDeleted += DirectoryRemoverOnDirectoryDeleted;

                        void DirectoryRemoverOnDirectoryDeleted(object? sender, string e)
                        {
                            if (settings.Verbose)
                            {
                                AnsiConsole.WriteLine(e);
                            }
                        }

                        directoryRemover.DeleteDirectory(directory, true);

                        if (settings.RemoveEmptyParentDirectories)
                        {
                            directoryRemover.DeleteParentDirectory(directory, settings.RemoveEmptyParentDirectories);
                        }
                    }
                    else
                    {
                        throw new ArgumentException(Resources.Exception_NonEmptyDirectory.Replace("{x}", directory));
                    }
                }
                else
                {
                    throw new DirectoryNotFoundException(Resources.Exception_DirectoryNotFound.Replace("{x}", directory));
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