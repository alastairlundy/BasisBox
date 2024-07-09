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

namespace NewDir.Library;

public static class NewDirectory
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="directoryPath"></param>
    /// <param name="unixFileMode"></param>
    /// <param name="createParentPaths"></param>
    /// <returns></returns>
    public static bool TryCreate(string directoryPath, UnixFileMode unixFileMode, bool createParentPaths)
    {
        try
        {
            Create(directoryPath, unixFileMode, createParentPaths);
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
    /// <param name="directoryPath"></param>
    /// <param name="unixCreateMode"></param>
    /// <param name="createParentPaths"></param>
    public static void Create(string directoryPath, UnixFileMode unixCreateMode, bool createParentPaths)
    {
        if (createParentPaths)
        {
            string[] directories = Directory.GetDirectories(directoryPath);

            foreach (string directory in directories)
            {
                if (!Directory.Exists(directory))
                {
                    if (OperatingSystem.IsWindows())
                    {
                        Directory.CreateDirectory(directory);
                    }
                    else
                    {
                        Directory.CreateDirectory(directory, unixCreateMode);
                    }
                }
            }
        }
        else
        {
            if (OperatingSystem.IsWindows())
            {
                Directory.CreateDirectory(directoryPath);
            }
            else
            {
                Directory.CreateDirectory(directoryPath, unixCreateMode);
            }
        }
    }
}