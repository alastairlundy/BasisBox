/*
    BasisBox - LoginName
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

using Spectre.Console;
using Spectre.Console.Cli;

namespace LoginName.Cli.Commands;

public class LoginNameCommand : Command<LoginNameCommand.Settings>
{
    public class Settings : CommandSettings
    {
        
        [CommandOption("--debug|--debugging")]
        [DefaultValue(false)]
        public bool UseDebugging { get; init; }
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        try
        {
            AnsiConsole.WriteLine(Environment.UserName);
            return 0;
        }
        catch(Exception exception)
        {
            if (settings.UseDebugging)
            {
                AnsiConsole.WriteException(exception);
            }
            else
            {
                AnsiConsole.WriteException(exception, ExceptionFormats.NoStackTrace);
            }
            
            return -1;
        }
    }
}