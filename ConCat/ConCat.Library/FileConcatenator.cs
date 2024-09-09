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

namespace ConCat.Library;

public static class FileConcatenator
{
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="files">The files to be concatenated.</param>
    /// <param name="addLineNumbers">Whether to add line numbers to the files.</param>
    /// <returns></returns>
    public static IEnumerable<string> ConcatenateToStringEnumerable(IEnumerable<string> files, bool addLineNumbers)
    {
        FileAppender fileAppender = new FileAppender(addLineNumbers);
        fileAppender.AppendFiles(files);

        return fileAppender.ToEnumerable();
    }

    /// <summary>
    /// Concatenates the contents of specified files and saves it to a new file.
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="files">The files to be concatenated.</param>
    /// <param name="addLineNumbers">Whether to add line numbers to the files.</param>
    /// <exception cref="Exception">Thrown </exception>
    public static void ConcatenateToFile(string filePath, IEnumerable<string> files, bool addLineNumbers)
    {
        try
        {
            File.WriteAllLines(filePath, ConcatenateToStringEnumerable(files, addLineNumbers));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}