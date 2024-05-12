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

public static class CharCounter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="files"></param>
    /// <returns></returns>
    public static ulong TotalCharCount(string[] files)
    {
        ulong[] characterCounts = new ulong[files.Length];
        for (int index = 0; index < files.Length; index++)
        {
            characterCounts[index] = CountCharacters(files[index]);
        }

        ulong totalCharacterCounts = ulong.MinValue;

        foreach (long count in characterCounts)
        {
            if (count == -1)
            {
                return ulong.MinValue;
            }
            
            totalCharacterCounts += Convert.ToUInt64(count);
        }

        return totalCharacterCounts;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    /// <exception cref="FileNotFoundException"></exception>
    public static ulong CountCharacters(string filePath)
    {
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);

            ulong charCount = 0;
            
            foreach (string line in lines)
            {
                string[] words = line.Split(' ');

                foreach (string word in words)
                {
                    char[] chars = word.ToCharArray();

                    charCount += Convert.ToUInt64(chars.Length);
                }
            }

            return charCount;
        }

        throw new FileNotFoundException(Resources.Exceptions_FileNotFound_Message, filePath);
    }
}