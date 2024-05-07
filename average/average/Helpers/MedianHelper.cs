using System;

using AlastairLundy.Extensions.System.Maths.Averages;

namespace average.Helpers;

internal class MedianHelper
{
    public static decimal GetMedian(string[] values)
    {
        return Median.OfDecimals(ConsoleHelper.ConvertInputToDecimal(values));
    }

    public static void PrintPrettyMedian(decimal median)
    {

    }
}
