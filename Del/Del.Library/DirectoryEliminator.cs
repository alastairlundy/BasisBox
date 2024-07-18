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
using System.IO;

using Del.Library.Localizations;

namespace Del.Library;

public class DirectoryEliminator
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="directory"></param>
    /// <param name="deleteEmptyDirectory"></param>
    /// <exception cref="DirectoryNotFoundException"></exception>
    public static void DeleteRecursively(string directory, bool deleteEmptyDirectory)
    {
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
                            File.Delete(file);
                        }
                    }

                    int numberOfFiles = Directory.GetFiles(directory).Length;

                    if (deleteEmptyDirectory == true && numberOfFiles == 0 || numberOfFiles > 0)
                    {
                        Directory.Delete(subDirectory);
                    }
                }
            }
            else
            {
                if (deleteEmptyDirectory)
                {
                    Directory.Delete(directory);
                }
            }
        }

        throw new DirectoryNotFoundException(Resources.Exceptions_DirectoryNotFound.Replace("{x}", directory));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="directory"></param>
    /// <param name="deleteEmptyDirectories"></param>
    /// <returns></returns>
    public static bool TryDeleteRecursively(string directory, bool deleteEmptyDirectories)
    {
        try
        {
            DeleteRecursively(directory, deleteEmptyDirectories);
            return true;
        }
        catch
        {
            return false;
        }
    }
}