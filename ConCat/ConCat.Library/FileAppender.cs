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
using System.Linq;

using AlastairLundy.Extensions.Collections.IEnumerables;

using CliUtilsLib;

using ConCat.Library.Localizations;

using NLine.Library;

// ReSharper disable RedundantIfElseBlock

namespace ConCat.Library;

/// <summary>
/// 
/// </summary>
public class FileAppender
{
    protected List<string> AppendedFileContents;
    
    public bool AddLineNumbers { get; protected set; }
    
    public FileAppender()
    {
        AppendedFileContents = new List<string>();
        AddLineNumbers = false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="addLineNumbers">Adds line numbers to the appended file contents.</param>
    public FileAppender(bool addLineNumbers)
    {
        AppendedFileContents = new List<string>();
        AddLineNumbers = addLineNumbers;
    }
    
    /// <summary>
    /// Attempts to append the contents of a file to an existing list.
    /// </summary>
    /// <param name="fileToBeAppended">The file to have its contents appended to the existing file contents. If no existing file contents exists, this will become the contents appended to in the future.</param>
    /// <returns>true if the files where successfully appended; returns false otherwise.</returns>
    public bool TryAppendFile(string fileToBeAppended)
    {
        try
        {
            AppendFile(fileToBeAppended);

            return true;
        }
        catch
        {
            return false;
        }
    }
    
    /// <summary>
    /// Appends the contents of a file to an existing list.
    /// </summary>
    /// <param name="fileToBeAppended">The file to be appended.</param>
    /// <exception cref="Exception">Thrown if the file appending fails.</exception>
    /// <exception cref="FileNotFoundException">Thrown if the file specified is not found.</exception>
    public void AppendFile(string fileToBeAppended)
    {
        if (FileFinder.IsAFile(fileToBeAppended) || File.Exists(fileToBeAppended))
        {
            try
            {
                string[] fileContents = File.ReadAllLines(fileToBeAppended);

                AppendFileContents(fileContents);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }
        else
        {
            throw new FileNotFoundException(Resources.Exceptions_FileNotFound, fileToBeAppended);
        }
    }

    /// <summary>
    /// Append the contents of an ordered enumerable of files to an existing list.
    /// </summary>
    /// <param name="filesToBeAppended">The files to be appended to the existing appended content.</param>
    public void AppendFiles(IOrderedEnumerable<string> filesToBeAppended)
    {
        AppendFiles(filesToBeAppended.ToArray());
    }

    /// <summary>
    /// Append the contents of the IEnumerable of files to an existing list.
    /// </summary>
    /// <param name="filesToBeAppended">The files to be appended to the existing appended content.</param>
    public void AppendFiles(IEnumerable<string> filesToBeAppended)
    {
        foreach (string file in filesToBeAppended)
        {
            AppendFile(file);
        }
    }

    /// <summary>
    /// Append the contents of a file to an existing list.
    /// </summary>
    /// <param name="fileContents">The file contents to be appended.</param>
    /// <exception cref="Exception">Thrown if the file appending fails.</exception>
    public void AppendFileContents(IEnumerable<string> fileContents)
    {
        try
        {
            if (AddLineNumbers)
            {
                AppendedFileContents = AppendedFileContents.Combine(LineNumberer.AddLineNumbers(fileContents, ". ")).ToList();
            }
            else
            {
                AppendedFileContents = AppendedFileContents.Combine(fileContents).ToList();
            }
        }
        catch (Exception exception)
        {
            throw new Exception(exception.Message, exception);
        }
    }

    /// <summary>
    /// Returns the appended contents to an IEnumerable.
    /// </summary>
    /// <returns>the list of appended strings as an enumerable.</returns>
    public IEnumerable<string> ToEnumerable()
    {
        return AppendedFileContents;
    }

    /// <summary>
    /// Writes the appended strings to a file.
    /// </summary>
    /// <param name="filePath">The path to save the file to.</param>
    /// <exception cref="ArgumentException">Thrown if an invalid file path is provided.</exception>
    /// <exception cref="FileNotFoundException">Thrown if the file specified is not found.</exception>
    /// <exception cref="Exception">Thrown if an exception occurs when attempting to write the file.</exception>
    public void WriteToFile(string filePath)
    {
        if (FileFinder.IsAFile(filePath))
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                File.WriteAllLines(filePath, AppendedFileContents);
            }
            catch (FileNotFoundException exception)
            {
                throw new FileNotFoundException(exception.Message, filePath, exception);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }
        else
        {
            throw new ArgumentException(Resources.Exceptions_NoFileProvided, filePath);
        }
    }
}