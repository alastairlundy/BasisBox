using WCount.Library.Localizations;

namespace WCount.Library;

public static class CharCounter
{
    /// <summary>
    /// Get the number of characters in a string.
    /// </summary>
    /// <param name="s">The string to be searched.</param>
    /// <returns>the number of characters in a string.</returns>
    public static ulong CountChars(this string s)
    {
        ulong totalChars = Convert.ToUInt64(s.ToCharArray().Length);

        return totalChars;
    }

    /// <summary>
    /// Gets the number of characters in a file.
    /// </summary>
    /// <param name="filePath">The file path of the file to be searched.</param>
    /// <returns>the number of characters in the file specified.</returns>
    /// <exception cref="FileNotFoundException">Thrown if the file specified could not be found.</exception>
    public static ulong CountCharsInFile(this string filePath)
    {
        if (File.Exists(filePath))
        {
            ulong totalChars = 0;
            
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                totalChars += CountChars(line.Split(' '));
            }

            return totalChars;
        }

        throw new FileNotFoundException(Resources.Exceptions_FileNotFound_Message, filePath);
    }

    /// <summary>
    /// Gets the number of characters in an IEnumerable of strings.
    /// </summary>
    /// <param name="enumerable">The IEnumerable to be searched.</param>
    /// <returns>the number of characters in the specified IEnumerable.</returns>
    public static ulong CountChars(this IEnumerable<string> enumerable)
    {
        ulong totalChars = 0;

        foreach (string s in enumerable)
        {
            totalChars += Convert.ToUInt64(s.ToCharArray().Length);
        }

        return totalChars;
    }
}