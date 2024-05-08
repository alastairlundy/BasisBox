using System;
using System.ComponentModel;
using System.Net.Http.Headers;

using Round.Cli.localizations;
using Round.Cli.Settings;

using Spectre.Console;
using Spectre.Console.Cli;

namespace Round.Cli.Commands;

internal class RoundCommand : Command<RoundCommand.Settings>
{
    public override int Execute(CommandContext context, Settings settings)
    {
        bool wasPrecisionProvided;
        decimal roundedValue;

        if (settings.NumberToRound == null)
        {
        }

        if (settings.NumberOfDecimalPlacesToUse == int.MinValue)
        {
            wasPrecisionProvided = false;
            roundedValue = decimal.Round((decimal)settings.NumberToRound, 2);
        }
        else
        {
            wasPrecisionProvided = true;
            
            roundedValue = decimal.Round((decimal)settings.NumberToRound, settings.NumberOfDecimalPlacesToUse);
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

            var grid = new Grid();

            grid.AddRow(
                new Padder[]
                {
                    new Padder(providedValueLabel).PadBottom(10),
                    new Padder(providedValueActual).PadBottom(10)
                });

            grid.AddRow(
                new Padder[] {
                    new Padder(providedPrecisionLabel).PadBottom(10),
                    new Padder(providedPrecisionActual).PadBottom(10)
            });
            grid.AddRow(
                new Padder[]
                {
                    new Padder(resultLabel).PadBottom(10),
                     new Padder(resultActual).PadBottom(10)
                });

            AnsiConsole.Write(grid);
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
