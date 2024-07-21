/*
     Copyright 2024 Alastair Lundy

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
 */

using WCount.Library.Localizations;

namespace WCount.Library;

public static class LineCounter
{
    /// <summary>
    /// Gets the number of lines in a file.
    /// </summary>
    /// <param name="filePath">The file path of the file to be searched.</param>
    /// <returns>the number of lines in a file.</returns>
    /// <exception cref="FileNotFoundException">Thrown if the file could not be located.</exception>
    public static ulong CountLinesInFile(this string filePath)
    {
        if (File.Exists(filePath))
        {
            return File.ReadAllLines(filePath).CountLines();
        }
        else
        {
            throw new FileNotFoundException(Resources.Exceptions_FileNotFound_Message, filePath);
        }
    }

    /// <summary>
    /// Gets the number of lines in a string.
    /// </summary>
    /// <param name="s">The string to be searched.</param>
    /// <returns>the number of lines in a string.</returns>
    public static ulong CountLines(this string s)
    {
        ulong totalCount = 0;
        
        foreach (char c in s)
        {
            if (c.Equals('\n') || c.Equals(char.Parse("\r\n")))
            {
                totalCount++;
            }
        }

        return totalCount;
    }

    /// <summary>
    /// Gets the number of lines in an IEnumerable of strings.
    /// </summary>
    /// <param name="enumerable">The IEnumerable to be searched.</param>
    /// <returns>the number of lines in the specified IEnumerable.</returns>
    public static ulong CountLines(this IEnumerable<string> enumerable)
    {
        ulong totalCount = 0;
        
        foreach (string s in enumerable)
        {
            totalCount += CountLines(s);
        }

        return totalCount;
    }
}