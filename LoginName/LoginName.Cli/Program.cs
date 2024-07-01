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

using LoginName.Cli.Commands;
using LoginName.Cli.Localizations;

using Spectre.Console.Cli;

CommandApp app = new();

app.Configure(config =>
{
    config.AddCommand<LoginNameCommand>("name")
        .WithAlias("")
        .WithDescription(Resources.Commands_LoginName_Description);

    config.SetApplicationName(Assembly.GetExecutingAssembly().GetName().Name!);
    config.UseAssemblyInformationalVersion();
});

app.SetDefaultCommand<LoginNameCommand>();

return app.Run(args);