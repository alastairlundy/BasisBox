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

using Del.Library.Localizations;

namespace Del.Library;

public class RecursiveDirectoryExplorer
{
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="directory"></param>
    /// <returns></returns>
    public (IEnumerable<string> files, IEnumerable<string> directories, IEnumerable<string> emptyDirectories) GetDirectoryContents(string directory)
    {
        return GetDirectoryContents(directory, true);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="directory"></param>
    /// <param name="includeEmptyDirectories"></param>
    /// <returns></returns>
    /// <exception cref="DirectoryNotFoundException"></exception>
    public (IEnumerable<string> files, IEnumerable<string> directories, IEnumerable<string> emptyDirectories) GetDirectoryContents(string directory, bool includeEmptyDirectories)
    {
        List<string> files = new List<string>();
        List<string> directories = new List<string>();
        List<string> emptyDirectories = new List<string>();
        
        if (Directory.Exists(directory))
        {
            if (Directory.GetDirectories(directory).Length > 0)
            {
                foreach (string subDirectory in Directory.GetDirectories(directory))
                {
                    if (Directory.GetFiles(subDirectory).Length > 0)
                    {
                        foreach (string file in Directory.GetFiles(subDirectory))
                        {
                            files.Add(file);
                        }
                    }

                    int numberOfFiles = Directory.GetFiles(directory).Length;

                    if (numberOfFiles > 0)
                    {
                        directories.Add(directory);
                    }
                    else if (includeEmptyDirectories == true && numberOfFiles == 0)
                    {
                        emptyDirectories.Add(directory);
                    }
                }
            }
            else
            {
                if (includeEmptyDirectories)
                {
                    emptyDirectories.Add(directory);
                }
            }

            return (files.ToArray(), directories.ToArray(), emptyDirectories.ToArray());
        }

        throw new DirectoryNotFoundException(Resources.Exceptions_DirectoryNotFound.Replace("{x}", directory));
    }
}