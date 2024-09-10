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

public static class CountCharactersExtensions
{
    /// <summary>
    /// Get the number of characters in a string.
    /// </summary>
    /// <param name="s">The string to be searched.</param>
    /// <returns>the number of characters in a string.</returns>
    public static ulong CountCharacters(this string s)
    {
        CharCounter charCounter = new CharCounter();
        return charCounter.CountCharacters(s);
    }

    /// <summary>
    /// Gets the number of characters in a file.
    /// </summary>
    /// <param name="filePath">The file path of the file to be searched.</param>
    /// <returns>the number of characters in the file specified.</returns>
    /// <exception cref="FileNotFoundException">Thrown if the file specified could not be found.</exception>
    public static ulong CountCharactersInFile(this string filePath)
    {
        if (File.Exists(filePath) == false)
        {
            throw new FileNotFoundException(Resources.Exceptions_FileNotFound_Message, filePath);
        }
        
        CharCounter charCounter = new CharCounter();
        return charCounter.CountCharactersInFile(filePath);
    }

    /// <summary>
    /// Gets the number of characters in an IEnumerable of strings.
    /// </summary>
    /// <param name="enumerable">The IEnumerable to be searched.</param>
    /// <returns>the number of characters in the specified IEnumerable.</returns>
    public static ulong CountCharacters(this IEnumerable<string> enumerable)
    {
        CharCounter charCounter = new CharCounter();
        return charCounter.CountCharacters(enumerable);
    }

}