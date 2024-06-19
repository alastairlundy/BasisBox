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

using System.Reflection;

using AlastairLundy.Extensions.System.AssemblyExtensions;
using AlastairLundy.Extensions.System.VersionExtensions;

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
    
    config.SetApplicationVersion(Assembly.GetExecutingAssembly().GetProjectVersion().ToFriendlyVersionString());
});

app.SetDefaultCommand<MainCommand>();

return app.Run(args);