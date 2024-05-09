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

using System;
using System.ComponentModel;
using Round.Cli.Helpers;
using Round.Cli.localizations;
using Round.Cli.Settings;

using Spectre.Console;
using Spectre.Console.Cli;

namespace Round.Cli.Commands;

internal class RoundCommand : Command<RoundCommand.Settings>
{
    public override int Execute(CommandContext context, Settings settings)
    {
        if (settings.Version != null)
        {
            ConsoleHelper.PrintVersion();
            return 0;
        }

        bool wasPrecisionProvided;
        decimal roundedValue;

        if (settings.NumberToRound == null)
        {
            AnsiConsole.WriteException(new NullReferenceException(Resources.Error_Number_NoInput), ExceptionFormats.ShowLinks);
            return -1;
        }

        if (settings.NumberOfDecimalPlacesToUse == int.MinValue)
        {
            wasPrecisionProvided = false;
            roundedValue = decimal.Round((decimal)settings.NumberToRound!, 2);
        }
        else
        {
            wasPrecisionProvided = true;
            roundedValue = decimal.Round((decimal)settings.NumberToRound!, settings.NumberOfDecimalPlacesToUse);
        }

        if (!settings.PrettyMode)
        {
            AnsiConsole.WriteLine(roundedValue.ToString());
            return 0;
        }
        else
        {
            var providedValueLabel = new Text($"{Resources.Input_ProvidedValue}:", new Style(Color.IndianRed)).LeftJustified();
            var providedValueActual = new Text(settings.NumberToRound.ToString()!, new Style(Color.DarkSeaGreen)).Centered();

            Text providedPrecisionLabel;

            if (wasPrecisionProvided)
            {
                providedPrecisionLabel = new Text($"{Resources.Input_Rounding_DecimalPlaces}:", new Style(Color.IndianRed)).LeftJustified();
            }
            else
            {
                providedPrecisionLabel = new Text($"{Resources.Input_Rounding_DecimalPlaces_NotProvided}:", new Style(Color.IndianRed)).LeftJustified();
            }

            var providedPrecisionActual = new Text(settings.NumberOfDecimalPlacesToUse.ToString(), new Style(Color.DarkSeaGreen)).Centered();

            var resultLabel = new Text($"{Resources.Input_RoundedValue}:", new Style(Color.IndianRed)).LeftJustified();
            var resultActual = new Text(roundedValue.ToString(), new Style(Color.Gold1)).Centered();

            AnsiConsole.Write(ConsoleGridHelper.CreateResultGrid(providedValueLabel, 
                providedValueActual, providedPrecisionLabel, providedPrecisionActual, resultLabel, resultActual));
            return 0;
        }
    }

    internal class Settings : BaseSettings
    {
        [CommandArgument(0, "<number_to_round>")]
        public decimal? NumberToRound { get; init; }

        [CommandOption("-dp|--decimal-places")]
        [DefaultValue(int.MinValue)]
        public int NumberOfDecimalPlacesToUse { get; init; }
    }
}