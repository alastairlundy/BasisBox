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
    /// <exception cref="FileNotFoundException">Thrown if the file could not be found.</exception>
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