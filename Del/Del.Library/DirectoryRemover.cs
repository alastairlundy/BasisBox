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

using Del.Library.Localizations;

namespace Del.Library;

public class DirectoryRemover
{
    public event EventHandler<string> DirectoryDeleted; 
    public event EventHandler<string> FileDeleted;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="directory"></param>
    /// <param name="deleteEmptyDirectory"></param>
    /// <returns></returns>
    public bool TryDeleteDirectory(string directory, bool deleteEmptyDirectory)
    {
        try
        {
            DeleteDirectory(directory, deleteEmptyDirectory);
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="directory"></param>
    /// <param name="deleteEmptyDirectory"></param>
    public void DeleteDirectory(string directory, bool deleteEmptyDirectory)
    {
        if (Directory.GetFiles(directory).Length == 0 &&
            Directory.GetDirectories(directory).Length == 0)
        {
            if (deleteEmptyDirectory)
            {
                Directory.Delete(directory);
                DirectoryDeleted?.Invoke(this, Resources.Directory_Deleted.Replace("{x}", directory));
            }
        }
        else
        {
            Directory.Delete(directory);
            DirectoryDeleted?.Invoke(this, Resources.Directory_Deleted.Replace("{x}", directory));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="directories"></param>
    /// <param name="deleteEmptyDirectory"></param>
    public void DeleteDirectories(IEnumerable<string> directories, bool deleteEmptyDirectory)
    {
        foreach (string directory in directories)
        {
            DeleteDirectory(directory, deleteEmptyDirectory);
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="directory"></param>
    /// <param name="deleteEmptyDirectory"></param>
    /// <exception cref="DirectoryNotFoundException"></exception>
    public void DeleteRecursively(string directory, bool deleteEmptyDirectory)
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
                            FileDeleted?.Invoke(this, Resources.File_Deleted.Replace("{x}", file));
                        }
                    }

                    int numberOfFiles = Directory.GetFiles(directory).Length;

                    if (deleteEmptyDirectory == true && numberOfFiles == 0 || numberOfFiles > 0)
                    {
                        Directory.Delete(subDirectory);

                        if (deleteEmptyDirectory == true && numberOfFiles == 0)
                        {
                            DirectoryDeleted?.Invoke(this, Resources.EmptyDirectory_Deleted.Replace("{x}", subDirectory));
                        }
                        else
                        {
                            DirectoryDeleted?.Invoke(this, Resources.Directory_Deleted.Replace("{x}", subDirectory));
                        }
                    }
                }
            }
            else
            {
                if (deleteEmptyDirectory)
                {
                    Directory.Delete(directory);
                    DirectoryDeleted?.Invoke(this, Resources.Directory_Deleted.Replace("{x}", directory));
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
    public bool TryDeleteRecursively(string directory, bool deleteEmptyDirectories)
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