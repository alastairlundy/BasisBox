/*

    BasisBox - ConCat
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

using ConCat.Cli.Localizations;

using Spectre.Console;

namespace ConCat.Cli.Helpers;

internal class ConsoleHelper
{
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