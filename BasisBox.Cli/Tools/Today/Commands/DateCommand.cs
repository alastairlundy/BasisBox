/*
    BasisBox - Today
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

using AlastairLundy.Extensions.System.Dates;

using Spectre.Console;
using Spectre.Console.Cli;

namespace BasisBox.Cli.Tools.Today.Commands;

public class DateCommand : Command<DateCommand.Settings>
{
    public class Settings : CommandSettings
    {
        
        [CommandOption("-d|--date")]
        [DefaultValue(null)]
        public string? DateToOperateOn { get; init; }
        
        [CommandOption("--debug|--debugging|--show-errros")]
        [DefaultValue(false)]
        public bool ShowErrors { get; init; }
    }

    internal string GetDateToOperateOn(string? input)
    {
        if (input == null || DateTime.TryParse(input, out DateTime dateTime) == false)
        {
            return DateTime.UtcNow.LongToday();
        }
        else if(DateTime.TryParse(input, out dateTime) == true)
        {
            return DateTime.Parse(input).LongToday();
        }
        else
        {
            throw new ArgumentException();
        }
    }
    
    public override int Execute(CommandContext context, Settings settings)
    {
        ExceptionFormats formats;

        if (settings.ShowErrors)
        {
            formats = ExceptionFormats.Default;
        }
        else
        {
            formats = ExceptionFormats.NoStackTrace;
        }
        
        try
        {
            string dateToOperateOn = GetDateToOperateOn(settings.DateToOperateOn);
            AnsiConsole.WriteLine(dateToOperateOn);

            return 0;
        }
        catch (Exception exception)
        {
            AnsiConsole.WriteLine("");
            
            AnsiConsole.WriteException(exception, formats);

            return -1;
        }
    }
}