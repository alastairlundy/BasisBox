﻿/*
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

using System;
using System.Collections.Generic;
using System.IO;

using Del.Library.Extensions;
using Del.Library.Localizations;

namespace Del.Library;

public class DirectoryRemover
{
    public DirectoryRemover()
    {
        
    }
    
    public event EventHandler<string> DirectoryDeleted; 
    public event EventHandler<string> FileDeleted;

    /// <summary>
    /// Attempts to delete the specified Directory.
    /// </summary>
    /// <param name="directory">The directory to be deleted.</param>
    /// <param name="deleteEmptyDirectory">Whether to delete the directory if it is empty or not.</param>
    /// <param name="deleteParentDirectory"></param>
    /// <returns>true if the directory was successfully deleted; returns false otherwise.</returns>
    public bool TryDeleteDirectory(string directory, bool deleteEmptyDirectory, bool deleteParentDirectory)
    {
        try
        {
            DeleteDirectory(directory, deleteEmptyDirectory, deleteParentDirectory);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Deletes a specified directory.
    /// </summary>
    /// <param name="directory">The directory to be deleted.</param>
    /// <param name="deleteEmptyDirectory">Whether to delete the directory or not if the directory is empty.</param>
    /// <param name="deleteParentDirectory"></param>
    /// <exception cref="DirectoryNotFoundException">Thrown if the directory does not exist or could not be located.</exception>
    public void DeleteDirectory(string directory, bool deleteEmptyDirectory, bool deleteParentDirectory)
    {
        if (Directory.Exists(directory))
        {
            if ((directory.IsDirectoryEmpty() && deleteEmptyDirectory) || !deleteEmptyDirectory)
            {
                    Directory.Delete(directory);
                    DirectoryDeleted?.Invoke(this, Resources.Directory_Deleted.Replace("{x}", directory));

                    if (deleteParentDirectory)
                    {
                       DeleteParentDirectory(directory, deleteEmptyDirectory);
                    }
            }
        }
        else
        {
            throw new DirectoryNotFoundException(Resources.Exceptions_DirectoryNotFound.Replace("{x}", directory));
        }
    }

    /// <summary>
    /// Deletes a parent directory of a directory.
    /// </summary>
    /// <param name="directory">The directory to get the parent directory of.</param>
    /// <param name="deleteEmptyDirectory">Whether to delete the parent directory if is empty or not.</param>
    /// <exception cref="DirectoryNotFoundException">Thrown if the directory does not exist or could not be located.</exception>
    public void DeleteParentDirectory(string directory, bool deleteEmptyDirectory)
    {
        if (Directory.Exists(directory))
        {
            if (directory.IsDirectoryEmpty() && deleteEmptyDirectory || !directory.IsDirectoryEmpty())
            {
                string parentDirectory = Directory.GetParent(directory)!.FullName;
                
                Directory.Delete(parentDirectory);
                DirectoryDeleted?.Invoke(this, Resources.Directory_Deleted.Replace("{x}", parentDirectory));
            }
        }

        throw new DirectoryNotFoundException(Resources.Exceptions_DirectoryNotFound.Replace("{x}", directory));
    }

    /// <summary>
    /// Deletes multiple specified directories.
    /// </summary>
    /// <param name="directories">The directories to be deleted.</param>
    /// <param name="deleteEmptyDirectory">Whether to delete empty directories or not.</param>
    /// <param name="deleteParentDirectory"></param>
    public void DeleteDirectories(IEnumerable<string> directories, bool deleteEmptyDirectory, bool deleteParentDirectory)
    {
        foreach (string directory in directories)
        {
            DeleteDirectory(directory, deleteEmptyDirectory, deleteParentDirectory);
        }
    }

    /// <summary>
    /// Deletes a directory recursively by deleting 
    /// </summary>
    /// <param name="directory">The directory to be recursively deleted.</param>
    /// <param name="deleteEmptyDirectory">Whether to delete empty directories or not.</param>
    /// <exception cref="DirectoryNotFoundException">Thrown if the directory does not exist or could not be located.</exception>
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
    /// Attempts to delete a directory recursively by deleting sub-folders and files before deleting the directory.
    /// </summary>
    /// <param name="directory">The parent directory to be deleted.</param>
    /// <param name="deleteEmptyDirectories">Whether to delete empty sub-folders or not.</param>
    /// <returns>true if the directory was recursively deleted successfully; returns false otherwise.</returns>
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