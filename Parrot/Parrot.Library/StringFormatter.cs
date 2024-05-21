using System.Text;

using AlastairLundy.Extensions.System.StringExtensions;

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
        input.RemoveEscapeCharacters();
        return input;
    }
}