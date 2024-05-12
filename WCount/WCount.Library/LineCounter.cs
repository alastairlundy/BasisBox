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

public static class LineCounter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="files"></param>
    /// <returns></returns>
    public static ulong TotalLineCount(string[] files)
    {
        int[] lineCounts = new int[files.Length];
        for (int index = 0; index < files.Length; index++)
        {
            lineCounts[index] = CountLinesInt(files[index]);
        }

        ulong totalLineCounts = 0;

        foreach (int lineCount in lineCounts)
        {
            if (lineCount == -1)
            {
                return ulong.MinValue;
            }
            
            totalLineCounts += Convert.ToUInt64(lineCount);
        }

        return totalLineCounts;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    /// <exception cref="FileNotFoundException"></exception>
    public static int CountLinesInt(string filePath)
    {
        if(File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);

            return lines.Length;
        }

        throw new FileNotFoundException(Resources.Exceptions_FileNotFound_Message, filePath);
    }

    public static ulong CountLinesLong(string filePath)
    {
        if (File.Exists(filePath))
        {
            ulong wordCount = 0;
            IEnumerable<string> lines = File.ReadLines(filePath);
            
            foreach (string line in lines)
            {
                wordCount++;
            }

            return wordCount;
        }

        throw new FileNotFoundException(Resources.Exceptions_FileNotFound_Message, filePath);
    }
}