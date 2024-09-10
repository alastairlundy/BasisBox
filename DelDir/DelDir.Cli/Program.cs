/*
    BasisBox - DelDir
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

using DelDir.Cli.Commands;

using Spectre.Console.Cli;

CommandApp app = new CommandApp();

app.Configure(config =>
{
    config.AddBranch("", branchConfig =>
    {
        branchConfig.AddCommand<DeleteDirectoryCommand>("")
            .WithAlias("delete-directory")
            .WithAlias("delete");

        branchConfig.AddCommand<DeleteManyDirectoriesCommand>("many")
            .WithAlias("multi")
            .WithAlias("delete-many")
            .WithAlias("delete-multi");
    });
    
    config.UseAssemblyInformationalVersion();
});

app.SetDefaultCommand<DeleteDirectoryCommand>();

return app.Run(args);