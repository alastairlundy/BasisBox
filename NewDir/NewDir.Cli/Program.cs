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

using System.Linq;
using System.Reflection;

using NewDir.Cli.Commands;
using NewDir.Cli.Localizations;

using Spectre.Console.Cli;

CommandApp app = new();

app.Configure(config =>
{
    config.AddCommand<NewDirCommand>("")
        .WithDescription(Resources.Commands_NewDir_Description);

    config.AddCommand<MultipleNewDirCommand>("many")
        .WithAlias("multiple")
        .WithAlias("multi")
        .WithDescription(Resources.Commands_ManyNewDir_Description);
    
    config.SetApplicationName(Assembly.GetExecutingAssembly().GetName().Name!);
    config.UseAssemblyInformationalVersion();
});

if (args.Length > 1 && !args.Contains("-"))
{
    app.SetDefaultCommand<MultipleNewDirCommand>();
}
else
{
    app.SetDefaultCommand<NewDirCommand>();
}

return app.Run(args);