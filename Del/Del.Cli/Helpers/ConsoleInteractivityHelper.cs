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

using Del.Cli.Localizations;

using Spectre.Console;

namespace Del.Cli.Helpers;

public class ConsoleInteractivityHelper
{
    public static bool DeleteFile(string fileName)
    {
        return Delete(Resources.DeleteConfrmation_File.Replace("{x}", fileName));
    }

    public static bool DeleteDirectory(string directoryName)
    {
        return Delete(Resources.DeleteConfirmation_Directory.Replace("{x}", directoryName));
    }
    
    internal static bool Delete(string message)
    {
        bool validInputProvided = false;
        do
        {
            AnsiConsole.Write($"{message}\t");

            string input = Console.ReadLine()!;

            AnsiConsole.WriteLine();
            
            input = input.ToLower();

            if (input.Equals(Resources.Input_Yes) || input.Equals("y"))
            {
                return true;
            }

            if (input.Equals(Resources.Input_No) || input.Equals("n"))
            {
                return false;
            }

            AnsiConsole.WriteLine(Resources.Exceptions_InvalidDeleteConfirmation);
            AnsiConsole.WriteLine();
        } while (validInputProvided == false);

        return false;
    }
}