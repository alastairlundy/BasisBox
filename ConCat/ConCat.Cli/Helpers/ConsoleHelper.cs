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
using ConCat.Cli.Localizations;
using Spectre.Console;

namespace ConCat.Cli.Helpers;

internal class ConsoleHelper
{
    internal static string[] AddLineNumbering(string[] lines)
    {
        string[] newLines = new string[lines.Length];

        for (int index = 0; index < lines.Length; index++)
        {
            newLines[index] = $"{index}: {lines[index]}";
        }

        return newLines;
    }

    internal static void PrintLines(string[] lines, bool includeLineNumber)
    {
        for (int index = 0; index < lines.Length; index++)
        {
            if (includeLineNumber)
            {
                AnsiConsole.WriteLine($"{index}: {lines[index]}");
            }
            else
            {
                AnsiConsole.WriteLine(lines[index]);
            }
        }
    }
    
    internal static int HandleException(Exception ex, string message, bool debuggingMode)
    {
        if (debuggingMode)
        {
            AnsiConsole.WriteException(ex);
        }
        else
        {
            AnsiConsole.WriteException(ex, ExceptionFormats.NoStackTrace);
        }
        
        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine(message);
        
        PrintBugReportRequest();
        return -1;
    }
    
    internal static void PrintBugReportRequest()
    {
        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine(Resources.Exceptions_BugReport_Request);
        AnsiConsole.WriteLine(Resources.Exceptions_BugReport_File.Replace("{x}", Constants.GitHubIssuesUrl));
    }

}