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
using System.IO;

using NewDir.Cli.Localizations;
using NewDir.Cli.Settings;
using NewDir.Library;

using Spectre.Console;
using Spectre.Console.Cli;

namespace NewDir.Cli.Commands;

public partial class MultipleNewDirCommand : Command<MultipleNewDirCommandSettings>
{
    public override int Execute(CommandContext context, MultipleNewDirCommandSettings settings)
    {
        ExceptionFormats exceptionFormat;
        
        if (settings.UseDebugging)
        {
            exceptionFormat = ExceptionFormats.Default;
        }
        else
        {
            exceptionFormat = ExceptionFormats.NoStackTrace;
        }
        
        if (settings.DirectoryNames == null)
        {
            AnsiConsole.WriteException(new ArgumentNullException(nameof(settings.DirectoryNames), Resources.Exceptions_DirectoryNotSpecified_Plural), exceptionFormat);
            return -1;
        }

        try
        {
            UnixFileMode? fileMode = PermissionHelper.GetUnixFileMode(settings.Mode);

            foreach (string directory in settings.DirectoryNames)
            {
                NewDirectory.Create(directory, (UnixFileMode)fileMode!, settings.CreateParentDirectories);
            }
            
            return 0;
        }
        catch (Exception exception)
        {
            AnsiConsole.WriteException(exception, exceptionFormat);
            return -1;
        }
    }
}