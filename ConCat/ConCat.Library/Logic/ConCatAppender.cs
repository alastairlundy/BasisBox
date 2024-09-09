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

namespace ConCat.Library.Logic;

public static class ConCatAppender
{
    public static IEnumerable<string> ToStringEnumerable(string fileToBeAddedTo, IEnumerable<string> filesToAppend,
        bool addLineNumbers)
    {
        FileAppender fileAppender = new FileAppender(addLineNumbers);

        fileAppender.AppendFile(fileToBeAddedTo);
        fileAppender.AppendFiles(filesToAppend);
        
        return fileAppender.ToEnumerable();
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileToBeAddedTo"></param>
    /// <param name="filesToAppend"></param>
    /// <param name="addLineNumbers"></param>
    /// <param name="filePath"></param>
    public static void ToNewFile(string fileToBeAddedTo, IEnumerable<string> filesToAppend,
        bool addLineNumbers, string filePath)
    {
        File.WriteAllLines(filePath, ToStringEnumerable(fileToBeAddedTo, filesToAppend, addLineNumbers));
    }
}