/*

    BasisBox - ConCat Library
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