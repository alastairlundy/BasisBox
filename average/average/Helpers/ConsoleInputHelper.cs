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

namespace Average.Helpers;

public static class ConsoleInputHelper
{
    public static bool IsStringAFileName(string input)
    {
        if (input.EndsWith(".txt") || input.EndsWith(".csv") || input.EndsWith(".rtf") || input.EndsWith(".xlsx"))
        {
            return true;
        }
        else
        {
            for(int index = 0; index < 10;  index++)
            {
                if (input.EndsWith($".{index}"))
                {
                    return false;
                }
            }
            
            return File.Exists(input);
        }
    }

    public static bool DoesInputContainFileNames(string[] values)
    {
        foreach (string value in values)
        {
            if(IsStringAFileName(value))
            {
                return true;
            }
        }

        return false;
    }

    public static string[] ExtractFileNamesFromInput(string[] values)
    {
        List<string> result = new List<string>();

        foreach(string value in values)
        {
            if (IsStringAFileName(value))
            {
                result.Add(value);
            }
        }
        return result.ToArray();
    }
}