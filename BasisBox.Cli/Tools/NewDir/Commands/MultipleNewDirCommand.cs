/*
    BasisBox - NewDir
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
using System.IO;

using AlastairLundy.Extensions.IO.Directories;

using NewDir.Cli.Settings;

using Spectre.Console;
using Spectre.Console.Cli;

namespace NewDir.Cli.Commands;

public class MultipleNewDirCommand : Command<MultipleNewDirCommandSettings>
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
            DirectoryCreator directoryCreator = new DirectoryCreator();
            
            UnixFileMode? fileMode = PermissionHelper.GetUnixFileMode(settings.Mode);

            foreach (string directory in settings.DirectoryNames)
            {
                directoryCreator.CreateDirectory(directory,  directory, (UnixFileMode)fileMode!, settings.CreateParentDirectories);
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