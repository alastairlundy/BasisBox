using System;


using AlastairLundy.Extensions.System.Maths.Averages;

namespace average.Helpers;

internal class ModeHelper
{

    public static decimal[] GetModes(string[] values)
    {
        return Mode.OfDecimals(ConsoleHelper.ConvertInputToDecimal(values));
    }

    public static void PrintPrettyMode(decimal[] values)
    {

    }
}
