/*
    BasisBox - DelDir
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
using System.ComponentModel;
using System.IO;
using System.Linq;
using AlastairLundy.Extensions.IO.Directories;
using BasisBox.Cli.Localizations;
using Spectre.Console;
using Spectre.Console.Cli;

namespace BasisBox.Cli.Tools.DelDir.Commands;

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
            AnsiConsole.WriteException(new ArgumentNullException(Resources.Exceptions_NoArgumentsProvided));
            return -1;
        }
        
        try
        {
            foreach (string directory in settings.DirectoriesToBeDeleted)
            {
                if (directory.Equals("/"))
                {
                    throw new ArgumentException(Resources.Exceptions_InvalidSlashCommand, directory);
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

                        directoryRemover.DeleteDirectoryRecursively(directory, true);

                        if (settings.RemoveEmptyParentDirectories)
                        {
                            directoryRemover.DeleteParentDirectory(directory, settings.RemoveEmptyParentDirectories);
                        }
                    }
                    else
                    {
                        throw new ArgumentException(Resources.Exceptions_DirectoryNotEmpty.Replace("{x}", directory));
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

                        directoryRemover.DeleteDirectory(directory, true, settings.RemoveEmptyParentDirectories);

                        if (settings.RemoveEmptyParentDirectories)
                        {
                            directoryRemover.DeleteParentDirectory(directory, settings.RemoveEmptyParentDirectories);
                        }
                    }
                    else
                    {
                        throw new ArgumentException(Resources.Exceptions_DirectoryNotEmpty.Replace("{x}", directory));
                    }
                }
                else
                {
                    throw new DirectoryNotFoundException(Resources.Exceptions_DirectoryNotFound.Replace("{x}", directory));
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