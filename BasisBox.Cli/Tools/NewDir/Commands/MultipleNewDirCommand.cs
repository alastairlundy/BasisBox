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

using BasisBox.Cli.Helpers;
using BasisBox.Cli.Localizations;

using BasisBox.Cli.Tools.NewDir.Helpers;
using BasisBox.Cli.Tools.NewDir.Settings;

using Spectre.Console;
using Spectre.Console.Cli;

namespace BasisBox.Cli.Tools.NewDir.Commands;

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
        
        int fileResult = FileArgumentHelpers.HandleFileArgument(settings.DirectoryNames, exceptionFormat);

        if (fileResult == -1)
        {
            return -1;
        }

        try
        {
            DirectoryCreator directoryCreator = new DirectoryCreator();
            
            UnixFileMode? fileMode = PermissionHelper.GetUnixFileMode(settings.Mode);

            foreach (string directory in settings.DirectoryNames!)
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