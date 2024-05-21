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

// See https://aka.ms/new-console-template for more information

using System;

using Round.Cli.Commands;
using Round.Cli.Localizations;

using Spectre.Console.Cli;

CommandApp app = new CommandApp();
app.Configure(config =>
{
    config.AddCommand<RoundDecimalCommand>("")
        .WithAlias("decimal-places")
        .WithAlias("dp")
        .WithDescription(Resources.Command_Round_Decimal_Description)
        .WithExample("1.6532", "2")
        .WithExample("3.1495265", "4");

    config.AddCommand<RoundSignificantFiguresCommand>("sf")
        .WithAlias("significant-figures")
        .WithAlias("significantfigures")
        .WithDescription(Resources.Command_Round_SignificantFigure_Description)
        .WithExample("0.19458", "2")
        .WithExample("5.44832", "4");

    //config.
});

app.SetDefaultCommand<RoundDecimalCommand>();

return app.Run(args);