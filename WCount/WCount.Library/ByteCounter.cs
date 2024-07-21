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

using System.Text;
using WCount.Library.Enums;
using WCount.Library.Localizations;

namespace WCount.Library;

public static class ByteCounter
{
    /// <summary>
    /// Gets the number of bytes in a string.
    /// </summary>
    /// <param name="s">The string to be searched.</param>
    /// <param name="textEncodingType">The type of encoding to use to decode the bytes.</param>
    /// <returns>the number of bytes in the string.</returns>
    /// <exception cref="ArgumentException">Thrown if the text encoding is not supported.</exception>
    public static int CountBytes(this string s, TextEncodingType textEncodingType)
    {
        byte[] bytes = textEncodingType switch
        {
            TextEncodingType.LATIN1 => Encoding.Latin1.GetBytes(s),
            TextEncodingType.UNICODE => Encoding.Unicode.GetBytes(s),
            TextEncodingType.ASCII => Encoding.ASCII.GetBytes(s),
            TextEncodingType.UTF7 => Encoding.UTF7.GetBytes(s),
            TextEncodingType.UTF8 => Encoding.UTF8.GetBytes(s),
            TextEncodingType.UTF32 => Encoding.UTF32.GetBytes(s),
            _ => throw new ArgumentException()
        };

        return bytes.Length;
    }

    /// <summary>
    /// Gets the number of bytes in a file.
    /// </summary>
    /// <param name="filePath">The file path of the file to be searched.</param>
    /// <param name="textEncodingType">The type of encoding to use to decode the bytes.</param>
    /// <returns>the number of bytes in a file.</returns>
    /// <exception cref="FileNotFoundException">Thrown if the file could not be located.</exception>
    public static ulong CountBytesInFile(this string filePath, TextEncodingType textEncodingType)
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
    public static ulong CountBytes(this IEnumerable<string> enumerable, TextEncodingType textEncodingType)
    {
        ulong totalBytes = 0;

        foreach (string s in enumerable)
        {
            totalBytes += Convert.ToUInt64(CountBytes(s, textEncodingType));
        }

        return totalBytes;
    }
}