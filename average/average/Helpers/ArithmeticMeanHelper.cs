using System;

using AlastairLundy.Extensions.System.Maths.Averages;

namespace average.Helpers;

internal class ArithmeticMeanHelper
{
    public static decimal GetArithmeticMean(string[] values)
    {
        return ArithmeticMean.ToDecimal(ConsoleHelper.ConvertInputToDecimal(values));
    }


}
