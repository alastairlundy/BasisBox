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

using NLine.Cli.Commands;
using NLine.Cli.Localizations;

using Spectre.Console.Cli;

CommandApp commandApp = new CommandApp();

commandApp.Configure(config =>
{
    config.AddCommand<LineNumberingCommand>("")
        .WithAlias("nl")
        .WithDescription(Resources.App_Description)
        .WithExample("-v 0")
        .WithExample("-w 5");

});