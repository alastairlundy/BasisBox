using System.Text;

namespace Parrot.Cli.Helpers;

public class StringFormatter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string RemoveEscapeChars(string input)
    {
        string newInput = input;

        string[] escapeChars = new[] { "\r", "\n", "\t", "\v", @"\c", @"\e", "\f", "\a", "\b", "\\", @"\NNN", @"\xHH"};

        for (int index = 0; index < escapeChars.Length; index++)
        {
            if (input.Contains(escapeChars[index]))
            {
                newInput = newInput.Replace(escapeChars[index], string.Empty);
            }
        }

        return newInput;
    }
}