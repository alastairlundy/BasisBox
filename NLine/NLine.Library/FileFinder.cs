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

// ReSharper disable UseIndexFromEndExpression
namespace NLine.Library;

public static class FileFinder
{
    /// <summary>
    /// A method to return any file names found within a specified string array.
    /// </summary>
    /// <param name="arguments">The string array to be checked.</param>
    /// <returns>the file(s) if one was provided in the list of arguments; returns null otherwise.</returns>
    public static string[]? FindFileNames(string[] arguments)
    {
        if (FoundAFileInArgs(arguments))
        {
            List<string> list = new();
            
            foreach (string arg in arguments)
            {
                if (arg.Length > 3)
                {
                    if (arg[arg.Length - 3].Equals('.') || arg[arg.Length - 2].Equals('.'))
                    {
                        list.Add(arg);
                    }
                }
                
                if (arg.EndsWith(".txt") || arg.EndsWith(".rtf"))
                {
                    list.Add(arg);
                }
            }

            return list.ToArray();
        }

        return null;
    }

    /// <summary>
    /// A method to determine if a file name is contained within a string array.
    /// </summary>
    /// <param name="arguments">The string array to be searched.</param>
    /// <returns>true if a file is found within the specified string array; returns false otherwise.</returns>
    public static bool FoundAFileInArgs(string[] arguments)
    {
        foreach (string arg in arguments)
        {
            if (arg.Length > 3)
            {
                    if (arg[arg.Length - 3].Equals('.') || arg[arg.Length - 2].Equals('.'))
                    {
                        return true;
                    }
            }
            
            if (arg.EndsWith(".txt") || arg.EndsWith(".rtf"))
            {
                return true;
            }
        }

        return false;
    }
}