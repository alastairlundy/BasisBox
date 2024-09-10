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
using System.Text;

using WCount.Library.Localizations;

namespace WCount.Library.Extensions;

public static class CountBytesExtensions
{
    /// <summary>
    /// Gets the number of bytes in a string.
    /// </summary>
    /// <param name="s">The string to be searched.</param>
    /// <param name="encoding">The type of encoding to use to decode the bytes.</param>
    /// <returns>the number of bytes in the string.</returns>
    public static int CountBytes(this string s, Encoding encoding)
    {
        ByteCounter byteCounter = new ByteCounter();
        return byteCounter.CountBytes(s, encoding);
    }

    /// <summary>
    /// Gets the number of bytes in a file.
    /// </summary>
    /// <param name="filePath">The file path of the file to be searched.</param>
    /// <param name="textEncodingType">The type of encoding to use to decode the bytes.</param>
    /// <returns>the number of bytes in a file.</returns>
    /// <exception cref="FileNotFoundException">Thrown if the file could not be located.</exception>
    public static ulong CountBytesInFile(this string filePath, Encoding textEncodingType)
    {
        if (File.Exists(filePath) == false)
        {
            throw new FileNotFoundException(Resources.Exceptions_FileNotFound_Message, filePath);
        }
        
        ByteCounter byteCounter = new ByteCounter();
        return byteCounter.CountBytesInFile(filePath, textEncodingType);
    }

    /// <summary>
    /// Gets the number of bytes in an IEnumerable of strings.
    /// </summary>
    /// <param name="enumerable">The IEnumerable to be searched.</param>
    /// <param name="textEncodingType">The type of encoding to use to decode the bytes.</param>
    /// <returns>the number of bytes in a specified IEnumerable.</returns>
    public static ulong CountBytes(this IEnumerable<string> enumerable, Encoding textEncodingType)
    {
        ByteCounter byteCounter = new ByteCounter();
        return byteCounter.CountBytes(enumerable, textEncodingType);
    }

}