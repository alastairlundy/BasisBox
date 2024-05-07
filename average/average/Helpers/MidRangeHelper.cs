using System;


using AlastairLundy.Extensions.System.Maths.Averages;

namespace average.Helpers;

internal class MidRangeHelper
{
    public static decimal GetMidRange(string[] values)
    {
        return MidRange.OfDecimals(ConsoleHelper.ConvertInputToDecimal(values));
    }


}