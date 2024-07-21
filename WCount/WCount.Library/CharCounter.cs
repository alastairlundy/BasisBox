using WCount.Library.Localizations;

namespace WCount.Library;

public static class CharCounter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static ulong CountChars(this string s)
    {
        ulong totalChars = Convert.ToUInt64(s.ToCharArray().Length);

        return totalChars;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    /// <exception cref="FileNotFoundException"></exception>
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
    /// 
    /// </summary>
    /// <param name="enumerable"></param>
    /// <returns></returns>
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