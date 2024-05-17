using System;

using Spectre.Console;

namespace ConCat.Cli.Helpers;

internal class ConsoleHelper
{
    internal static void PrintLines(string[] lines, bool includeLineNumber)
    {
        for (int index = 0; index < lines.Length; index++)
        {
            if (includeLineNumber)
            {
                AnsiConsole.WriteLine($"{index}: {lines[index]}");
            }
            else
            {
                AnsiConsole.WriteLine(lines[index]);
            }
        }
    }

}