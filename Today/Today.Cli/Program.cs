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

using AlastairLundy.Extensions.System.VersionExtensions;

using Spectre.Console.Cli;
using Today.Cli.Commands;

CommandApp app = new CommandApp();

app.Configure(config =>
{
    config.AddBranch("date", configurator =>
    {
        configurator.AddCommand<DateCommand>("").WithAlias("date");
        
        configurator.AddCommand<DisplayDateCommand>("display");
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