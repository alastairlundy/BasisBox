/*
    BasisBox - Del
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
using System.Linq;

namespace BasisBox.Cli.Tools.Del.Helpers;

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