/*
    BasisBox - NewDir Library
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
using System.IO;

namespace NewDir.Library;

public static class NewDirectory
{
    /// <summary>
    /// Attempts to create a new directory with the specified parameters.
    /// </summary>
    /// <param name="directoryPath">The path of the directory to be created.</param>
    /// <param name="unixFileMode">The file mode to use to create the directory. Only used on Unix based systems.</param>
    /// <param name="createParentPaths">Whether to create parent directory paths, if required, when creating the new directory.</param>
    /// <returns>true if the directory was successfully created; returns false otherwise.</returns>
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
    /// Creates a new directory with the specified paramaters.
    /// </summary>
    /// <param name="directoryPath">The path of the directory to be created.</param>
    /// <param name="unixFileMode">The file mode to use to create the directory. Only used on Unix based systems.</param>
    /// <param name="createParentPaths">Whether to create parent directory paths, if required, when creating the new directory.</param>
    public static void Create(string directoryPath, UnixFileMode unixFileMode, bool createParentPaths)
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
                        Directory.CreateDirectory(directory, unixFileMode);
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
                Directory.CreateDirectory(directoryPath, unixFileMode);
            }
        }
    }
}