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