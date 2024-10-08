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

using System;
using BasisBox.Cli.Localizations;
using Spectre.Console;

namespace BasisBox.Cli.Tools.Del.Helpers;

public static class InteractiveInputHelper
{
    public static bool DeleteFile(string fileName)
    {
        return Delete(Resources.Del_App_Any_DeleteConfirmation_File.Replace("{x}", fileName));
    }

    public static bool DeleteDirectory(string directoryName)
    {
        return Delete(Resources.Del_App_Any_DeleteConfirmation_YesOrNo.Replace("{x}", directoryName));
    }
    
    internal static bool Delete(string message)
    {
        bool output = false;
        
        bool validInputProvided = false;
        do
        {
            AnsiConsole.WriteLine();
            AnsiConsole.Write($"{message}\t");

            string input = Console.ReadLine()!;

            AnsiConsole.WriteLine();
            
            input = input.ToLower();

            if (input.Equals(Resources.ConCat_App_Inputs_Yes) || input.Equals("y"))
            {
                output = true;
                validInputProvided = true;
            }

            if (input.Equals(Resources.ConCat_App_Inputs_No) || input.Equals("n"))
            {
                output = false;
                validInputProvided = true;
            }

            AnsiConsole.WriteLine(Resources.Exceptions_InvalidDeleteConfirmation);
            AnsiConsole.WriteLine();
        } while (validInputProvided == false);

        return output;
    }
}