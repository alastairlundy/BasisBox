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
using DelDir.Cli.Localizations;

using Spectre.Console;
using Spectre.Console.Cli;

namespace DelDir.Cli.Commands;

public class DeleteDirectoryCommand : Command<DeleteDirectoryCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<Directory>")]
        public string? DirectoryToBeDeleted { get; init; }
        
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
        
        if (settings.DirectoryToBeDeleted == null)
        {
            AnsiConsole.WriteException(new ArgumentNullException(nameof(settings.DirectoryToBeDeleted), Resources.), exceptionFormats);
        }

        try
        {
            if (Directory.Exists(settings.DirectoryToBeDeleted))
            {
                if (Directory.GetDirectories(settings.DirectoryToBeDeleted).Any() &&
                    Directory.GetFiles(settings.DirectoryToBeDeleted).Any())
                {
                    
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