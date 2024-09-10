/*
    BasisBox - Del Library
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
using System.IO;

using Del.Library.Localizations;

namespace Del.Library;

/// <summary>
/// 
/// </summary>
public static class RecursiveDirectoryExplorer
{
    
    /// <summary>
    /// Gets the directories and files within a parent directory.
    /// </summary>
    /// <param name="directory">The directory to be searched.</param>
    /// <returns>the directories and files within a parent directory.</returns>
    public static (IEnumerable<string> files, IEnumerable<string> directories, IEnumerable<string> emptyDirectories) GetDirectoryContents(string directory)
    {
        return GetDirectoryContents(directory, true);
    }
    
    /// <summary>
    /// Gets the directories and files within a parent directory.
    /// </summary>
    /// <param name="directory">The directory to be searched.</param>
    /// <param name="includeEmptyDirectories">Whether to include empty directories or not.</param>
    /// <returns>the directories and files within a parent directory.</returns>
    /// <exception cref="DirectoryNotFoundException">Thrown if the directory does not exist.</exception>
    public static (IEnumerable<string> files, IEnumerable<string> directories, IEnumerable<string> emptyDirectories) GetDirectoryContents(string directory, bool includeEmptyDirectories)
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