/*

    BasisBox - ConCat
    Copyright (C) 2024 Alastair Lundy

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Lesser General Public License as published by
    the Free Software Foundation, version 3 of the License.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU Lesser General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System.Linq;
using System.Reflection;

using AlastairLundy.Extensions.System;

using CliUtilsLib;
using ConCat.Cli.Commands;

using Spectre.Console.Cli;

CommandApp app = new();

app.Configure(config =>
{
    config.AddCommand<ConcatenateCommand>("concatenate");
    
    config.AddCommand<AppendCommand>("append");
    
    config.AddCommand<DisplayCommand>("display")
        .WithAlias("cat");

    config.AddCommand<CopyCommand>("copy");

    config.AddCommand<NewFileCommand>("new");
    
    config.SetApplicationVersion(Assembly.GetExecutingAssembly().GetName().Version!.ToFriendlyVersionString());
});

if (args.Contains(">>"))
{
    app.SetDefaultCommand<AppendCommand>();
}
else if (args.Contains(">"))
{
    if (FileArgumentFinder.GetNumberOfFilesFoundInArgs(args) == 2)
    {
        app.SetDefaultCommand<CopyCommand>();
    }
    else
    {
        if (args[0].StartsWith('>') && args.Length == 1)
        {
            app.SetDefaultCommand<NewFileCommand>();
        }
        else
        {
            app.SetDefaultCommand<ConcatenateCommand>();
        }
    }
}
else if (!args.Contains(">>") && !args.Contains(">"))
{
    app.SetDefaultCommand<DisplayCommand>();
}


return app.Run(args);