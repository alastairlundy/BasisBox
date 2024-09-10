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