using System;
using System.Reflection;

using AlastairLundy.Extensions.System.AssemblyExtensions;
using AlastairLundy.Extensions.System.VersionExtensions;

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

    public static void PrintUnformattedVersion()
    {
        Console.WriteLine($"{Assembly.GetExecutingAssembly().GetProjectName()} {Assembly.GetExecutingAssembly().GetProjectVersion().GetFriendlyVersionToString()}");
    }

    public

    public static void PrintUnformattedLicense()
    {


        foreach (string line in File.ReadAllLines($"Environment.CurrentDirectory{Path.DirectorySeparatorChar}LICENSE.txt"))
        {
            Console.WriteLine(line);
        }

        Console.WriteLine();
    }
}
