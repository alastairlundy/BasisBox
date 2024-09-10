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

using System.IO;

using AlastairLundy.Extensions.System;

using Del.Library.Localizations;

namespace Del.Library.Extensions;

public static class IsEmptyExtensions
{
    
    /// <summary>
    /// Checks if a Directory is empty or not.
    /// </summary>
    /// <param name="directory">The directory to be searched.</param>
    /// <returns>true if the directory is empty; returns false otherwise.</returns>
    /// <exception cref="DirectoryNotFoundException">Thrown if the directory does not exist.</exception>
    public static bool IsDirectoryEmpty(this string directory)
    {
        if (Directory.Exists(directory))
        {
            return Directory.GetFiles(directory).Length == 0;
        }
        else
        {
            throw new DirectoryNotFoundException(Resources.Exceptions_DirectoryNotFound.Replace("{x}", directory));
        }
    }

    /// <summary>
    /// Determines whether subdirectories of a directory are empty.
    /// </summary>
    /// <param name="directory">The directory to be searched.</param>
    /// <returns>true if all subdirectories in a directory are empty; returns false otherwise.</returns>
    /// <exception cref="DirectoryNotFoundException">Thrown if the directory does not exist or could not be located.</exception>
    public static bool AreSubdirectoriesEmpty(this string directory)
    {
        if (!Directory.Exists(directory))
        {
            throw new DirectoryNotFoundException(Resources.Exceptions_DirectoryNotFound.Replace("{x}", directory));
        }
        
        string[] subDirectories = Directory.GetDirectories(directory);
                
        bool[] allowRecursiveEmptyDirectoryDeletion = new bool[subDirectories.Length];

        for (int i = 0; i < subDirectories.Length; i++)
        {
            string dir = subDirectories[i];
                    
            if (dir.IsDirectoryEmpty())
            {
                allowRecursiveEmptyDirectoryDeletion[i] = true;
            }
            else
            {
                allowRecursiveEmptyDirectoryDeletion[i] = false;
            }
        }

        return allowRecursiveEmptyDirectoryDeletion.IsAllTrue();
    }
}