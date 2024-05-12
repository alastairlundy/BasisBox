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
using WCount.Library.localizations;

namespace WCount.Library;

public static class ByteCounter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="files"></param>
    /// <returns></returns>
    public static ulong TotalByteCount(string[] files)
    {
        ulong[] lineCounts = new ulong[files.Length];
        for (int index = 0; index < files.Length; index++)
        {
            lineCounts[index] = CountBytes(files[index]);
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
    public static ulong CountBytes(string filePath)
    {
        if (File.Exists(filePath))
        {
            ulong byteCount = 0;
            
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] words = line.Split(' ');

                foreach (string word in words)
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(word);

                    byteCount += Convert.ToUInt64(bytes.Length);
                }
            }

            return byteCount;
        }

        throw new FileNotFoundException(Resources.Exceptions_FileNotFound_Message, filePath);
    }
}