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
using System.Linq;

namespace Del.Cli.Helpers;

public static class InteractiveRecursiveDeletionHelper
{
    public static IEnumerable<string> GetFilesToBeDeleted(IEnumerable<string> files)
    {
        List<string> filesToBeDeleted = new List<string>();
        
        foreach (string file in files.ToArray())
        {
            bool deleteFile = InteractiveInputHelper.DeleteFile(file);

            if (deleteFile == true)
            {
                filesToBeDeleted.Add(file);
            }
        }

        return filesToBeDeleted;
    }
    
    public static IEnumerable<string> GetDirectoriesToBeDeleted(IEnumerable<string> directories)
    {
        List<string> directoriesToBeDeleted = new List<string>();
        
        foreach (string directory in directories.ToArray())
        {
            bool deleteDirectory = InteractiveInputHelper.DeleteDirectory(directory);

            if (deleteDirectory == true)
            {
                directoriesToBeDeleted.Add(directory);
            }
        }

        return directoriesToBeDeleted;
    }
}