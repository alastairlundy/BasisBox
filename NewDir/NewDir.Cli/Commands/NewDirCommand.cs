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

        ExceptionFormats exceptionFormat;
        
        if (settings.UseDebugging)
        {
            exceptionFormat = ExceptionFormats.Default;
        }
        else
        {
            exceptionFormat = ExceptionFormats.NoStackTrace;
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
                bool isNumericNotation = UnixFilePermissionConverter.IsNumericNotation(settings.Mode);
                bool isSymbolicNotation = UnixFilePermissionConverter.IsSymbolicNotation(settings.Mode);
                
                if (isNumericNotation && !isSymbolicNotation)
                {
                    fileMode = UnixFilePermissionConverter.ParseNumericValue(settings.Mode);
                }
                else if (isSymbolicNotation && !isNumericNotation)
                {
                    fileMode = UnixFilePermissionConverter.ParseSymbolicValue(settings.Mode);
                }
                else
                {
                    fileMode = UnixFileMode.UserRead & UnixFileMode.UserWrite;
                }
            }

            NewDirectory.Create(settings.DirectoryName, fileMode, settings.CreateParentDirectories);
            return 0;
        }
        catch (UnauthorizedAccessException exception)
        {
            AnsiConsole.WriteException(exception, exceptionFormat);
            return -1;
        }
        catch (PathTooLongException pathTooLongException)
        {
            AnsiConsole.WriteException(pathTooLongException, exceptionFormat);
            return -1;
        }
        catch (IOException exception)
        {
            AnsiConsole.WriteException(exception, exceptionFormat);
            return -1;
        }
        catch (Exception exception)
        {
            AnsiConsole.WriteException(exception, exceptionFormat);
            return -1;
        }
    }
}