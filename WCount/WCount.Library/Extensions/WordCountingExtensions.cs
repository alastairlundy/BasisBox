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

namespace WCount.Library.Extensions;

public static class WordCountingExtensions
{
    /// <summary>
    /// Gets the number of words in a string.
    /// </summary>
    /// <param name="s">The string to be searched.</param>
    /// <returns>The number of words in the provided string.</returns>
    public static ulong CountWords(this string s)
    {
        WordCounter wordCounter = new WordCounter();
        return wordCounter.CountWords(s);
    }

    /// <summary>
    /// Gets the number of words in a file.
    /// </summary>
    /// <param name="filePath">The file path of the file to be searched.</param>
    /// <returns>The number of words in the file.</returns>
    public static ulong CountWordsInFile(this string filePath)
    {
        WordCounter wordCounter = new WordCounter();
        return wordCounter.CountWordsInFile(filePath);
    }

    /// <summary>
    /// Gets the number of words in an IEnumerable of strings.
    /// </summary>
    /// <param name="enumerable">The IEnumerable of strings to be searched.</param>
    /// <returns>the number of words in an IEnumerable of strings.</returns>
    public static ulong CountWords(this IEnumerable<string> enumerable)
    {
        WordCounter wordCounter = new WordCounter();
        return wordCounter.CountWords(enumerable);
    }
}