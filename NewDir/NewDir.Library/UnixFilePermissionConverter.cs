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

using NewDir.Library.Localizations;

namespace NewDir.Library;

public static class UnixFilePermissionConverter
{
    /// <summary>
    /// Detects whether a Unix Octal file permission notation is valid.
    /// </summary>
    /// <param name="notation">The numeric notation to be compared.</param>
    /// <returns>true if a valid unix file permission octal notation has been provided; returns false otherwise.</returns>
    public static bool IsNumericNotation(string notation)
    {
        if (notation.Length == 4 && int.TryParse(notation, out int result))
        {
            return result switch
            {
                0 or 111 or 222 or 333 or 444 or 555 or 666 or 700 or 740 or 777 => true,
                _ => false
            };
        }

        return false;
    }

    /// <summary>
    /// Detects whether a Unix symbolic file permission is valid.
    /// </summary>
    /// <param name="notation">The symbolic notation to be compared.</param>
    /// <returns>true if a valid unix file permission symbolic notation has been provided; returns false otherwise.</returns>
    public static bool IsSymbolicNotation(string notation)
    {
        if (notation.Length == 10)
        {
            return notation switch
            {
                "----------" or
                    "---x--x--x" or
                    "--w--w--w-" or
                    "--wx-wx-wx" or
                    "-r--r--r--" or
                    "-r-xr-xr-x" or
                    "-rw-rw-rw-" or
                    "-rwx------" or
                    "-rwxr-----" or
                    "-rwxrwx---" or
                    "-rwxrwxrwx" => true,
                _ => false
            };
        }

        return false;
    }
    
    /// <summary>
    /// Converts a Unix file permission in symbolic notation to Octal notation.
    /// </summary>
    /// <param name="symbolicNotation">The symbolic notation to be converted to octal notation.</param>
    /// <returns>the octal notation equivalent of the specified symbolic notation.</returns>
    /// <exception cref="ArgumentException">Thrown if an invalid symbolic notation is specified.</exception>
    public static string ToNumericNotation(string symbolicNotation)
    {
        if (symbolicNotation.Length == 10)
        {
            return symbolicNotation switch
            {
                "----------" => "0000",
                "---x--x--x" => "0111",
                "--w--w--w-" => "0222",
                "--wx-wx-wx" => "0333",
                "-r--r--r--" => "0444",
                "-r-xr-xr-x" => "0555",
                "-rw-rw-rw-" => "0666",
                "-rwx------" => "0700",
                "-rwxr-----" => "0740",
                "-rwxrwx---" => "0770",
                "-rwxrwxrwx" => "0777",
                _ => throw new ArgumentException(Resources.Exceptions_Permissions_InvalidSymbolicNotation)
            };
        }

        throw new ArgumentException(Resources.Exceptions_Permissions_InvalidSymbolicNotation);
    }

    /// <summary>
    /// Converts a Unix file permission in symbolic notation to Octal notation.
    /// </summary>
    /// <param name="numericNotation">The octal notation to be converted to symbolic notation.</param>
    /// <returns>the symbolic notation equivalent of the specified octal notation.</returns>
    /// <exception cref="ArgumentException">Thrown if an invalid octal notation is specified.</exception>
    public static string ToSymbolicNotation(string numericNotation)
    {
        if (numericNotation.Length == 4 && int.TryParse(numericNotation, out int result))
        {
            return result switch
            {
                0 => "----------",
                111 => "---x--x--x",
                222 => "--w--w--w-",
                333 => "--wx-wx-wx",
                444 => "-r--r--r--",
                555 => "-r-xr-xr-x",
                666 => "-rw-rw-rw-",
                700 => "-rwx------",
                740 => "-rwxr-----",
                770 => "-rwxrwx---",
                777 => "-rwxrwxrwx",
                _ => throw new ArgumentException(Resources.Exceptions_Permissions_InvalidNumericNotation)
            };
        }

        throw new ArgumentException(Resources.Exceptions_Permissions_InvalidNumericNotation);
    }
    
    /// <summary>
    /// Parse a Unix file permission in octal notation to a UnixFileMode enum.
    /// </summary>
    /// <param name="permissionNotation">The octal notation to be parsed.</param>
    /// <returns>the UnixFileMode enum equivalent to the specified octal notation.</returns>
    /// <exception cref="ArgumentException">Thrown if an invalid octal notation is specified.</exception>
    public static UnixFileMode ParseNumericValue(string permissionNotation)
    {
        if (permissionNotation.Length == 4 && int.TryParse(permissionNotation, out int result))
        {
            return result switch
            {
                0 => UnixFileMode.None,
                111 => UnixFileMode.UserExecute,
                222 => UnixFileMode.UserWrite,
                333 => UnixFileMode.UserWrite & UnixFileMode.UserExecute,
                444 => UnixFileMode.UserRead,
                555 => UnixFileMode.UserRead & UnixFileMode.UserExecute,
                666 => UnixFileMode.UserRead & UnixFileMode.UserWrite,
                700 => UnixFileMode.UserRead & UnixFileMode.UserWrite & UnixFileMode.UserExecute,
                740 => UnixFileMode.UserExecute & UnixFileMode.UserWrite & UnixFileMode.UserRead &
                       UnixFileMode.GroupRead,
                770 => UnixFileMode.UserRead & UnixFileMode.UserWrite & UnixFileMode.UserExecute &
                       UnixFileMode.GroupRead & UnixFileMode.GroupWrite & UnixFileMode.GroupExecute,
                777 => UnixFileMode.UserRead & UnixFileMode.UserWrite & UnixFileMode.UserExecute &
                       UnixFileMode.GroupRead & UnixFileMode.GroupWrite & UnixFileMode.GroupExecute &
                       UnixFileMode.OtherRead & UnixFileMode.OtherWrite & UnixFileMode.OtherExecute,
                _ => throw new ArgumentException(Resources.Exceptions_Permissions_InvalidNumericNotation)
            };
        }

        throw new ArgumentException(Resources.Exceptions_Permissions_InvalidNumericNotation);
    }
    
    /// <summary>
    /// Attempts to parse a Unix file permission in Octal notation to a UnixFileMode enum. 
    /// </summary>
    /// <param name="permissionNotation">The Unix file permission octal notation to be parsed.</param>
    /// <param name="fileMode">The UnixFileMode equivalent value to the octal notation if a valid octal notation was specified; null otherwise.</param>
    /// <returns>true if a valid Unix file permission octal notation was specified; returns false otherwise.</returns>
    public static bool TryParseNumericValue(string permissionNotation, out UnixFileMode? fileMode)
    {
        try
        {
            fileMode = ParseNumericValue(permissionNotation);
            return true;
        }
        catch
        {
            fileMode = null;
            return false;
        }
    }

    /// <summary>
    /// Parse a Unix file permission in symbolic notation to a UnixFileMode enum.
    /// </summary>
    /// <param name="permissionNotation">The symbolic notation to be compared.</param>
    /// <returns>the UnixFileMode enum equivalent to the specified symbolic notation.</returns>
    /// <exception cref="ArgumentException">Thrown if an invalid symbolic notation is specified.</exception>
    public static UnixFileMode ParseSymbolicValue(string permissionNotation)
    {
        if (permissionNotation.Length == 10)
        {
            return permissionNotation.ToLower() switch
            {
                "----------" => UnixFileMode.None,
                "---x--x--x" => UnixFileMode.UserExecute,
                "--w--w--w-" => UnixFileMode.UserWrite,
                "--wx-wx-wx" => UnixFileMode.UserWrite & UnixFileMode.UserExecute,
                "-r--r--r--" => UnixFileMode.UserRead,
                "-r-xr-xr-x" => UnixFileMode.UserRead & UnixFileMode.UserExecute,
                "-rw-rw-rw-" => UnixFileMode.UserRead & UnixFileMode.UserWrite,
                "-rwx------" => UnixFileMode.UserRead & UnixFileMode.UserWrite & UnixFileMode.UserExecute,
                "-rwxr-----" => UnixFileMode.UserExecute & UnixFileMode.UserWrite & UnixFileMode.UserRead &
                                UnixFileMode.GroupRead,
                "-rwxrwx---" => UnixFileMode.UserRead & UnixFileMode.UserWrite & UnixFileMode.UserExecute &
                                UnixFileMode.GroupRead & UnixFileMode.GroupWrite & UnixFileMode.GroupExecute,
                "-rwxrwxrwx" => UnixFileMode.UserRead & UnixFileMode.UserWrite & UnixFileMode.UserExecute &
                                UnixFileMode.GroupRead & UnixFileMode.GroupWrite & UnixFileMode.GroupExecute &
                                UnixFileMode.OtherRead & UnixFileMode.OtherWrite & UnixFileMode.OtherExecute,
                _ => throw new ArgumentException(Resources.Exceptions_Permissions_InvalidSymbolicNotation)
            };
        }

        throw new ArgumentException(Resources.Exceptions_Permissions_InvalidSymbolicNotation);
    }

    /// <summary>
    /// Attempts to parse a Unix file permission in Symbolic notation to a UnixFileMode enum. 
    /// </summary>
    /// <param name="permissionNotation">The Unix file permission symbolic notation to be parsed.</param>
    /// <param name="fileMode">The UnixFileMode equivalent value to the symbolic notation if a valid symbolic notation was specified; null otherwise.</param>
    /// <returns>true if a valid Unix file permission symbolic notation was specified; returns false otherwise.</returns>
    public static bool TryParseSymbolicValue(string permissionNotation, out UnixFileMode? fileMode)
    {
        try
        {
            fileMode = ParseSymbolicValue(permissionNotation);
            return true;
        }
        catch
        {
            fileMode = null;
            return false;
        }
    }

    /// <summary>
    /// Attempts to parse a Unix file permission in either Numeric or Symbolic notation to a UnixFileMode enum. 
    /// </summary>
    /// 
    /// <param name="permissionNotation">The Unix file permission symbolic notation to be parsed.</param>
    /// <param name="fileMode">The unix file mode if a valid permission notation was parsed; null otherwise.</param>
    /// <returns>true if the file mode notation was parsed successfully; returns false otherwise.</returns>
    public static bool TryParse(string permissionNotation, out UnixFileMode? fileMode)
    {
        bool isNumericNotation = IsNumericNotation(permissionNotation);
        bool isSymbolicNotation = IsSymbolicNotation(permissionNotation);

        try
        {
            if (isNumericNotation && !isSymbolicNotation)
            {
                fileMode = ParseNumericValue(permissionNotation);
            }
            else if (isSymbolicNotation && !isNumericNotation)
            {
                fileMode = ParseSymbolicValue(permissionNotation);
            }
            else
            {
                fileMode = UnixFileMode.UserRead & UnixFileMode.UserWrite;
            }

            return true;
        }
        catch
        {
            fileMode = null;
            return false;
        }
    }
}