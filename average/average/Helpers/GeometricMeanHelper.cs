using System;

using AlastairLundy.Extensions.System.Maths.Averages;

namespace average.Helpers;

internal class GeometricMeanHelper
{
    public static decimal GetGeometricMean(string[] values)
    {
        return GeometricMean.ToDecimal(ConsoleHelper.ConvertInputToDecimal(values));
    }


}