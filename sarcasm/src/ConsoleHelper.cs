namespace Sarcasm;

public class ConsoleHelper
{
    public static void PrintLicenseToConsole()
    {
        string[] licenseStrings =
            File.ReadAllLines(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "LICENSE.txt");

        foreach (string str in licenseStrings)
        {
            Console.WriteLine(str);
        }

        Console.WriteLine();
    }
    
    public static void PrintResults(string[] results)
    {
        foreach (string str in results)
        {
            Console.WriteLine(str);
        }
    }
}