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

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using WCount.Library.Interfaces;
using WCount.Library.Localizations;

namespace WCount.Library;

public class ByteCounter : IByteCounter
{
    /// <summary>
    /// Gets the number of bytes in a string.
    /// </summary>
    /// <param name="s">The string to be searched.</param>
    /// <param name="textEncodingType">The type of encoding to use to decode the bytes.</param>
    /// <returns>the number of bytes in the string.</returns>
    /// <exception cref="ArgumentException">Thrown if the text encoding is not supported.</exception>
    public int CountBytes(string s, Encoding textEncodingType)
    {
        byte[] bytes;
        
        if (Equals(textEncodingType, Encoding.Latin1))
        {
            bytes = Encoding.Latin1.GetBytes(s);
        }
        else if (Equals(textEncodingType, Encoding.Unicode))
        {
            bytes = Encoding.Unicode.GetBytes(s);
        }
        else if (Equals(textEncodingType, Encoding.UTF32))
        {
            bytes = Encoding.UTF32.GetBytes(s);
        }
        else if (Equals(textEncodingType, Encoding.UTF8))
        {
            bytes = Encoding.UTF8.GetBytes(s);
        }
        else if (Equals(textEncodingType, Encoding.UTF7))
        {
            throw new NotSupportedException(Resources.Exceptions_UTF7NotSupported);
        }
        else if (Equals(textEncodingType, Encoding.ASCII))
        {
            bytes = Encoding.ASCII.GetBytes(s);
        }
        else if (Equals(textEncodingType, Encoding.BigEndianUnicode))
        {
            bytes = Encoding.BigEndianUnicode.GetBytes(s);
        }
        else
        {
            throw new ArgumentException(Resources.Exceptions_EncodingNotSupported);
        }

        return bytes.Length;
    }

    /// <summary>
    /// Gets the number of bytes in a file.
    /// </summary>
    /// <param name="filePath">The file path of the file to be searched.</param>
    /// <param name="textEncodingType">The type of encoding to use to decode the bytes.</param>
    /// <returns>the number of bytes in a file.</returns>
    /// <exception cref="FileNotFoundException">Thrown if the file could not be located.</exception>
    public ulong CountBytesInFile(string filePath, Encoding textEncodingType)
    {
        if (File.Exists(filePath))
        {
            return CountBytes(File.ReadAllLines(filePath), textEncodingType);
        }
        else
        {
            throw new FileNotFoundException(Resources.Exceptions_FileNotFound_Message, filePath);
        }
    }

    /// <summary>
    /// Gets the number of bytes in an IEnumerable of strings.
    /// </summary>
    /// <param name="enumerable">The IEnumerable to be searched.</param>
    /// <param name="textEncodingType">The type of encoding to use to decode the bytes.</param>
    /// <returns>the number of bytes in a specified IEnumerable.</returns>
    public ulong CountBytes(IEnumerable<string> enumerable, Encoding textEncodingType)
    {
        ulong totalBytes = 0;

        foreach (string s in enumerable)
        {
            totalBytes += Convert.ToUInt64(CountBytes(s, textEncodingType));
        }

        return totalBytes;
    }
}