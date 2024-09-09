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
        if (args[0].StartsWith('>') && args.Count() == 1)
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