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

using NewDir.Cli.Localizations;
using NewDir.Cli.Settings;

using NewDir.Library;

using Spectre.Console;
using Spectre.Console.Cli;

namespace NewDir.Cli.Commands;

public partial class NewDirCommand : Command<NewDirCommandSettings>
{
    public override int Execute(CommandContext context, NewDirCommandSettings settings)
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
        
        if (settings.DirectoryName == null)
        {
            AnsiConsole.WriteException(new ArgumentNullException(Resources.Exceptions_DirectoryNotSpecified), exceptionFormat);
            return -1;
        }
        
        try
        {
            UnixFileMode? fileMode = PermissionHelper.GetUnixFileMode(settings.Mode);

            if (settings.DirectoryName.Split(' ').Length > 0)
            {
                foreach (string directory in settings.DirectoryName.Split(' '))
                {
                    NewDirectory.Create(directory, (UnixFileMode)fileMode!, settings.CreateParentDirectories);
                }
                return 0;
            }
            else
            {
                NewDirectory.Create(settings.DirectoryName, (UnixFileMode)fileMode!, settings.CreateParentDirectories);
                return 0;
            }
            
            
        }
        catch (Exception exception)
        {
            AnsiConsole.WriteException(exception, exceptionFormat);
            return -1;
        }
    }
}