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

using System.Reflection;

using AlastairLundy.Extensions.System;

using Spectre.Console.Cli;

using Today.Cli.Commands;

CommandApp app = new CommandApp();

app.Configure(config =>
{
    config.AddBranch("date", configurator =>
    {
        configurator.AddCommand<DateCommand>("").WithAlias("date");
        
//        configurator.AddCommand<DisplayDateCommand>("display");
        configurator.AddCommand<SetDateCommand>("set");

    });

    config.AddBranch("timezone", configurator =>
    {
        configurator.AddCommand<TimeZoneTimeCommand>("time");
        configurator.AddCommand<SetTimeZoneCommand>("set");
        configurator.AddCommand<DisplayTimeZoneCommand>("display");

    });

    config.SetApplicationName(Assembly.GetExecutingAssembly()!.GetName().Name!);
    config.SetApplicationVersion(Assembly.GetExecutingAssembly()!.GetName().Version.ToFriendlyVersionString());
});

app.SetDefaultCommand<DateCommand>();

return app.Run(args);