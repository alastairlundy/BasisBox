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
using Spectre.Console;
using Spectre.Console.Cli;
using Today.Library.Extensions;

namespace Today.Cli.Commands;

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
        }
        
        throw new NotImplementedException();
    }
}