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

public static class WordCounter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
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
    /// 
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    /// <exception cref="FileNotFoundException"></exception>
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
    /// 
    /// </summary>
    /// <param name="words"></param>
    /// <returns></returns>
    public static ulong CountWords(this IEnumerable<string> words)
    {
        ulong totalCount = 0;
        
        foreach (string s in words)
        {
            var _words = s.Split(' ');

            if (_words.Length > 0)
            {
                totalCount += Convert.ToUInt64(_words.Length);
            }
        }

        return totalCount;
    }
}