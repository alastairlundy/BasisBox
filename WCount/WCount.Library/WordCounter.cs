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

using System;
using System.Collections.Generic;
using System.IO;

using WCount.Library.Localizations;

namespace WCount.Library;

public static class WordCounter
{
    /// <summary>
    /// Gets the number of words in a string.
    /// </summary>
    /// <param name="s">The string to be searched.</param>
    /// <returns>The number of words in the provided string.</returns>
    public static ulong CountWords(this string s)
    {
        ulong totalCount = 0;
        
        string[] words = s.Split(' ');

        if (words.Length > 0)
        {
            totalCount += Convert.ToUInt64(words.Length);
        }
        else
        {
            if (s.Length > 0)
            {
                totalCount = 1;
            }
        }

        return totalCount;
    }
    
    /// <summary>
    /// Gets the number of words in a file.
    /// </summary>
    /// <param name="filePath">The file path of the file to be searched.</param>
    /// <returns>The number of words in the file.</returns>
    /// <exception cref="FileNotFoundException">Thrown if the file could not be found.</exception>
    public static ulong CountWordsInFile(this string filePath)
    {
        if (File.Exists(filePath))
        {
            ulong wordCount = 0;
            
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                wordCount += CountWords(line);
            }

            return wordCount;
        }
        else
        {
            throw new FileNotFoundException(Resources.Exceptions_FileNotFound_Message, filePath);
        }
    }

    /// <summary>
    /// Gets the number of words in an IEnumerable of strings.
    /// </summary>
    /// <param name="enumerable">The IEnumerable of strings to be searched.</param>
    /// <returns>the number of words in an IEnumerable of strings.</returns>
    public static ulong CountWords(this IEnumerable<string> enumerable)
    {
        ulong totalCount = 0;
        
        foreach (string s in enumerable)
        {
            var words = s.Split(' ');

            if (words.Length > 0)
            {
                totalCount += Convert.ToUInt64(words.Length);
            }
        }

        return totalCount;
    }
}