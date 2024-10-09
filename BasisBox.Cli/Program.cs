/*
    BasisBox
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

using BasisBox.Cli.Localizations;

using BasisBox.Cli.Tools.ConCat.Commands;
using BasisBox.Cli.Tools.Del.Commands;
using BasisBox.Cli.Tools.DelDir.Commands;
using BasisBox.Cli.Tools.LoginName.Commands;
using BasisBox.Cli.Tools.NewDir.Commands;
using BasisBox.Cli.Tools.NLine;
using BasisBox.Cli.Tools.NLine.Commands;

using BasisBox.Cli.Tools.WCount;
using BasisBox.Cli.Tools.WCount.Commands;

using CliUtilsLib;
using Spectre.Console.Cli;

CommandApp commandApp = new CommandApp();

commandApp.Configure(config =>
{
    //
    // NLine: Adds line numbering to IEnumerable<string> in the style of the ``nl`` unix command.
    //
    config.AddCommand<LineNumberingCommand>("nline")
        .WithAlias("nl")
        .WithDescription(Resources.NLine_App_Description)
        .WithExample("-v 0")
        .WithExample("-w 5");

    //
    // WCount: 
    //
    config.AddCommand<WCountCommand>("wcount")
        .WithAlias("wc")
        .WithDescription(Resources.WCount_App_Description)
        .WithExample("/path/to/example.txt")
        .WithExample("-l /path/to/foo.txt")
        .WithExample("/Path/To/foo.txt", "/Path/To/bar.txt");

    config.AddCommand<LoginNameCommand>("loginname")
        .WithAlias("uname")
        .WithDescription(Resources.LoginName_App_Description);

    config.AddBranch("newdir", conf =>
    {
        conf.AddCommand<NewDirCommand>("")
        .WithAlias("single")
        .WithDescription(Resources.NewDir_App_Commands_SingleNewDir_Description);

        conf.AddCommand<MultipleNewDirCommand>("many")
        .WithAlias("multi")
        .WithAlias("multiple")
        .WithDescription(Resources.NewDir_App_Commands_ManyNewDirs_Description);

        if (args.Length > 1)
        {
            conf.SetDefaultCommand<MultipleNewDirCommand>();
        }
        else
        {
            conf.SetDefaultCommand<NewDirCommand>();
        }
    });

    config.AddBranch("deldir", conf =>
    {
        conf.AddCommand<DeleteDirectoryCommand>("");
        
        conf.AddCommand<DeleteManyDirectoriesCommand>("many")
            .WithAlias("multiple")
            .WithAlias("multi");

        conf.SetDefaultCommand<DeleteDirectoryCommand>();
    });
    
    config.AddBranch("del", conf =>
    {
        conf.AddCommand<DeleteCommand>("")
        .WithAlias("single");

        conf.AddCommand<DeleteManyCommand>("many")
        .WithAlias("multiple")
        .WithAlias("multi");

        if (FileArgumentFinder.GetNumberOfFilesFoundInArgs(args) == 1 || args.Length == 1)
        {
            conf.SetDefaultCommand<DeleteCommand>();
        }
        else
        {
            conf.SetDefaultCommand<DeleteManyCommand>();
        }
    });
    
    config.AddBranch("concat", conf =>
    {
        conf.AddCommand<ConcatenateCommand>("concatenate");
    
        conf.AddCommand<AppendCommand>("append");
    
        conf.AddCommand<DisplayCommand>("display")
            .WithAlias("cat");

        conf.AddCommand<CopyCommand>("copy");

        conf.AddCommand<NewFileCommand>("new");
        
        conf.SetDefaultCommand<DisplayCommand>();
    });  
    
    config.UseAssemblyInformationalVersion();
});

return commandApp.Run(args);