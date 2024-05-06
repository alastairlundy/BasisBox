using System;

namespace average.Helpers;

internal class ConsoleHelper
{
    internal static decimal[] ConvertInputToDecimal(string[] values)
    {
        List<decimal> newValues = new List<decimal>();

        foreach (string value in values)
        {
            newValues.Add(decimal.Parse(value));
        }

        return newValues.ToArray();
    }

    public static void PrintUnformattedStr(string str)
    {
        Console.WriteLine(str);
    }

    public static void PrintUnformattedStrArray(string[] array)
    {
        foreach (string value in array)
        {
            Console.WriteLine(value);
        }
    }

    public static void PrintVersion()
    {

    }

    public static void PrintUnformattedLicense()
    {


        foreach (string line in File.ReadAllLines($"Environment.CurrentDirectory{Path.DirectorySeparatorChar}LICENSE.txt"))
        {
            Console.WriteLine(line);
        }

        Console.WriteLine();
    }
}
