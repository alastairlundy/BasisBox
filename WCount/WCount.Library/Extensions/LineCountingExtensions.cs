/*
    BasisBox - WCount Library
    Copyright (C) 2024 Alastair Lundy

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Lesser General Public License as published by
    the Free Software Foundation, version 3 of the License.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Lesser General Public License for more details.

    You should have received a copy of the GNU Lesser General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System.Collections.Generic;
using System.IO;

using WCount.Library.Localizations;

namespace WCount.Library.Extensions;

public static class LineCountingExtensions
{
    /// <summary>
    /// Gets the number of lines in a file.
    /// </summary>
    /// <param name="filePath">The file path of the file to be searched.</param>
    /// <returns>the number of lines in a file.</returns>
    /// <exception cref="FileNotFoundException">Thrown if the file could not be located.</exception>
    public static ulong CountLinesInFile(this string filePath)
    {
        if (File.Exists(filePath) == false)
        {
            throw new FileNotFoundException(Resources.Exceptions_FileNotFound_Message, filePath);
        }
        
        LineCounter lineCounter = new LineCounter();
        return lineCounter.CountLinesInFile(filePath);
    }

    /// <summary>
    /// Gets the number of lines in a string.
    /// </summary>
    /// <param name="s">The string to be searched.</param>
    /// <returns>the number of lines in a string.</returns>
    public static ulong CountLines(this string s)
    {
        LineCounter lineCounter = new LineCounter();
        return lineCounter.CountLines(s);
    }

    /// <summary>
    /// Gets the number of lines in an IEnumerable of strings.
    /// </summary>
    /// <param name="enumerable">The IEnumerable to be searched.</param>
    /// <returns>the number of lines in the specified IEnumerable.</returns>
    public static ulong CountLines(this IEnumerable<string> enumerable)
    {
        LineCounter lineCounter = new LineCounter();
        return lineCounter.CountLines(enumerable);
    }
}