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



using Average.Commands;
using Average.Commands.CalculationCommands;
using Average.Localizations;

using Spectre.Console.Cli;

CommandApp app = new CommandApp();
app.Configure(config =>
{
    config.AddCommand<MedianCommand>("median")
        .WithDescription(Resources.Command_Median_Description)
        .WithExample("1", "2", "3", "4", "5");

    config.AddCommand<ModeCommand>("mode")
       .WithDescription(Resources.Command_Mode_Description)
       .WithExample("1", "3", "3", "5", "7");

    config.AddCommand<ArithmeticMeanCommand>("arithmetic-mean")
       .WithAlias("mean")
       .WithAlias("arithmetic mean")
       .WithAlias("arithmetic")
       .WithDescription(Resources.Command_ArithmeticMean_Description)
       .WithExample("1", "4", "6", "8", "9", "13");

    config.AddCommand<GeometricMeanCommand>("geometric-mean")
       .WithAlias("geometric mean")
       .WithAlias("geometric")
       .WithAlias("geomean")
       .WithDescription(Resources.Command_GeometricMean_Description)
       .WithExample("1", "4", "6", "8", "9", "13");

    config.AddCommand<MidRangeCommand>("midrange")
       .WithDescription(Resources.Command_MidRange_Description)
       .WithExample("1", "3", "6", "9");

    config.AddCommand<InterquartileMeanCommand>("interquartertile-mean")
       .WithAlias("iqr-mean")
       .WithDescription(Resources.Command_InterquartileMean_Description)
       .WithExample("1", "2", "3", "4", "5", "6");

    config.AddCommand<VersionCommand>("version")
        .WithAlias("--version")
        .WithAlias("-v")
        .WithDescription(Resources.Command_Version_Description);
});
