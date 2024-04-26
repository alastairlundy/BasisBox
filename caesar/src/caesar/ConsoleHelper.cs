using caesar.localizations;

namespace caesar;

public class ConsoleHelper
{
    public static void PrintResults(string[] results)
    {
        foreach (string value in results)
        {
            Console.WriteLine(value);
        }
    }

    public static string[] ParseInputs(string? file = null, string[]? words = null)
    {
        string[] values;
        
        if (file != null && (file.EndsWith(".txt") || file.EndsWith(".rtf")))
        {
            values = File.ReadAllLines(file);
        }
        else
        {
            if (words != null)
            {
                values = words;
            }
            else
            {
                throw new ArgumentException(Resources.Exception_Argument_NoWords);
            }
        }

        return values;
    }
}