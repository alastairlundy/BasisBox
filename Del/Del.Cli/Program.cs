/*
    BasisBox - Del
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

using CliUtilsLib;

using Del.Cli.Commands;
using Del.Cli.Localizations;

using Spectre.Console.Cli;

CommandApp app = new CommandApp();

app.Configure(config =>
{
    config.AddCommand<DeleteCommand>("")
        .WithAlias("delete")
        .WithDescription(Resources.Command_Delete_Single_Description);

    config.AddCommand<DeleteManyCommand>("many")
        .WithAlias("delete-many")
        .WithDescription(Resources.Command_Delete_Many_Description);
    
    config.UseAssemblyInformationalVersion();
});

if (FileArgumentFinder.GetNumberOfFilesFoundInArgs(args) == 1 || args.Length == 1)
{
    app.SetDefaultCommand<DeleteCommand>();
}
else
{
    app.SetDefaultCommand<DeleteManyCommand>();
}

return app.Run(args);