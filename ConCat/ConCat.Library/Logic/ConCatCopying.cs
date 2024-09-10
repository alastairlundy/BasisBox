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
using System.IO;
using System.Linq;

using NLine.Library;

namespace ConCat.Library.Logic;

public static class ConCatCopying
{
    /// <summary>
    /// </summary>
    /// <param name="existingFile">The file to be copied</param>
    /// <param name="newFile">The destination file.</param>
    /// <param name="addLineNumbering">Adds line numbers to the appended file contents.</param>
    /// <exception cref="UnauthorizedAccessException"></exception>
    /// <exception cref="FileNotFoundException"></exception>
    /// <exception cref="Exception"></exception>
    public static void CopyFile(string existingFile, string newFile, bool addLineNumbering)
    {
        try
        {
            string[] newFileContents = File.ReadAllLines(existingFile);

            if (File.Exists(existingFile))
            {
                File.Delete(existingFile);
            }

            if (addLineNumbering)
            {
                newFileContents = LineNumberer.AddLineNumbers(newFileContents, ". ").ToArray();

                File.WriteAllLines(newFile, newFileContents);
            }
            else
            {
                File.Copy(existingFile, newFile);
            }
        }
        catch (UnauthorizedAccessException exception)
        {
            throw new UnauthorizedAccessException(exception.Message, exception);
        }
        catch (FileNotFoundException exception)
        {
            throw new FileNotFoundException(exception.Message, exception.FileName, exception);
        }
        catch(Exception exception)
        {
            throw new Exception(exception.Message, exception);
        }
    }
}