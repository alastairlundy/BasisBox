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
using System.Linq;

namespace ConCat.Library.Logic;

public static class ConCatAppender
{
    /// <summary>
    /// Appends the contents of files.
    /// </summary>
    /// <param name="files">The files to be appended.</param>
    /// <param name="addLineNumbering">Adds line numbers to the appended file contents.</param>
    /// <returns>the appended file contents as an IEnumerable of strings.</returns>
    /// <exception cref="UnauthorizedAccessException"></exception>
    /// <exception cref="FileNotFoundException"></exception>
    /// <exception cref="Exception"></exception>
    public static IEnumerable<string> AppendFiles(IEnumerable<string> files, bool addLineNumbering)
    {
        try
        {
            FileAppender fileAppender = new FileAppender(addLineNumbering);
        
            foreach (string file in files)
            {
                fileAppender.AppendFile(file);
            }
                  
            return fileAppender.ToEnumerable();
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
    
    /// <summary>
    /// Appends the contents of files.
    /// </summary>
    /// <param name="existingFiles">The files to </param>
    /// <param name="newFiles"></param>
    /// <param name="addLineNumbering">Adds line numbers to the appended file contents.</param>
    /// <returns></returns>
    /// <exception cref="UnauthorizedAccessException"></exception>
    /// <exception cref="FileNotFoundException"></exception>
    /// <exception cref="Exception"></exception>
    public static IEnumerable<string> AppendFiles(IEnumerable<string> existingFiles, IEnumerable<string> newFiles, bool addLineNumbering)
    {
              try
              {
                  FileAppender fileAppender = new FileAppender(addLineNumbering);

                  string[] filesToBeAppended = newFiles as string[] ?? newFiles.ToArray();
                  
                  fileAppender.AppendFiles(filesToBeAppended);
                  fileAppender.AppendFiles(existingFiles);
                  
                  return fileAppender.ToEnumerable();
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