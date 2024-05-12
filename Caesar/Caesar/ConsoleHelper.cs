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

using System.Security.Cryptography;

using Spectre.Console;

namespace Caesar;

public static class ConsoleHelper
{
    public static void PrintResults(string[] results)
    {
        var rows = new List<Text>();

        foreach (string result in results)
        {
            rows.Add(new Text(result));
        }
        
        AnsiConsole.Write(new Rows(rows));
    }

    public static int ShiftHandler(int? shiftAmount)
    {
        int shift = 0;
        
        if (shiftAmount == null)
        {
            shift = RandomNumberGenerator.GetInt32(1, 26);
        }
        else
        {
            shift = (int)shiftAmount;
        }

        return shift;
    }

    public static void SaveResultsToFile(string filePath, string[] results)
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllLines(filePath, results);
        }
        AnsiConsole.WriteException(new ArgumentException());
    }
}