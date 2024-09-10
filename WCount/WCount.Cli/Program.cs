/*
    BasisBox - WCount
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

using WCount.Cli.Commands;
using WCount.Cli.Localizations;

CommandApp app = new CommandApp();

app.Configure(config =>
{
    config.AddCommand<MainCommand>("")
        .WithDescription(Resources.App_Description)
        .WithExample("/path/to/example.txt")
        .WithExample("-l /path/to/foo.txt")
        .WithExample("/Path/To/foo.txt", "/Path/To/bar.txt");
    
    config.SetApplicationVersion(Assembly.GetExecutingAssembly().GetName().Version.ToFriendlyVersionString());
});

app.SetDefaultCommand<MainCommand>();

return app.Run(args);