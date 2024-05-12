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

using WCount.Library.localizations;

namespace WCount.Library;

public static class WordCounter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="files"></param>
    /// <returns></returns>
    public static ulong TotalWordCount(string[] files)
    {
        long[] wordCounts = new long[files!.Length];
        for (int index = 0; index < files.Length; index++)
        {
            wordCounts[index] =  CountWords(files[index]);
        }
            
        ulong totalWordCounts = 0;

        foreach (int lineCount in wordCounts)
        {
            if (lineCount == -1)
            {
                return ulong.MinValue;
            }
            
            totalWordCounts += Convert.ToUInt64(lineCount);
        }

        return totalWordCounts;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    /// <exception cref="FileNotFoundException"></exception>
    public static long CountWords(string filePath)
    {
        if (File.Exists(filePath))
        {
            long wordCount = 0;
            
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] words = line.Split(' ');

                wordCount += Convert.ToInt64(words.Length);
            }

            return wordCount;
        }

        throw new FileNotFoundException(Resources.Exceptions_FileNotFound_Message, filePath);
    }
}