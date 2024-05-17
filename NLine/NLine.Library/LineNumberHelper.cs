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

using System.Text;

using AlastairLundy.Extensions.System.BoolArrayExtensions;

namespace NLine.Library;

public class LineNumberHelper
{

    protected static int CalculateLineNumber(int currentIndex, int lineIncrementor, int initialLineNumber)
    {
        return currentIndex == 0 ? initialLineNumber : initialLineNumber * ((currentIndex + 1) * lineIncrementor);
    }

    protected static string AddColumns(int columnNumber)
    {
        StringBuilder stringBuilder = new StringBuilder();
        
        if (columnNumber > 0)
        {
            for (int column = 1; column <= columnNumber; column++)
            {
                stringBuilder.Append(' ');
            }

            return stringBuilder.ToString();
        }
        // ReSharper disable once RedundantIfElseBlock
        else
        {
            return string.Empty;
        }
    }

    protected static string AddLeadingZeroes(int lineNumberDigits)
    {
        StringBuilder stringBuilder = new StringBuilder();
        
        int zeroesToAdd = 5 - lineNumberDigits;

        if (zeroesToAdd > 0)
        {
            for (int zero = 0; zero < zeroesToAdd; zero++)
            {
                stringBuilder.Append('0');
            }

            return stringBuilder.ToString();
        }

        return string.Empty;
    }

    protected static bool NextXLinesIsEmpty(int numberOfLines, int currentIndex, string[] lines)
    {
        if (lines[currentIndex].Equals(string.Empty))
        {
            bool[] checkedLines = new bool[numberOfLines];
            
            bool linesChecked = false;
            int internalIndex = 1;
            
            while (!linesChecked)
            {
                int lineToBeChecked = currentIndex + internalIndex;
                // ReSharper disable once ArrangeRedundantParentheses
                if ((lines.Length > lineToBeChecked) && lineToBeChecked <= numberOfLines)
                {
                    checkedLines[lineToBeChecked] = lines[lineToBeChecked] == string.Empty;
                }

                if (lineToBeChecked > numberOfLines)
                {
                    linesChecked = true;
                }
            }

            return checkedLines.IsAllTrue();
        }

        return false;
    }

    protected static string AddTabSpacesIfNeeded(bool addTabSpaces, string line)
    {
        if (addTabSpaces)
        {
            return $"\t {line}";
        }
        else
        {
            return line;
        }
    }

    protected static string ConstructLine(int lineNumber, int columnNumber, string line, string lineNumberAppendedText, bool addTabSpaces, bool addLeadingZeroes)
    {
        StringBuilder stringBuilder = new StringBuilder();
        
        stringBuilder.Append(AddColumns(columnNumber));

        if (addLeadingZeroes)
        {
            stringBuilder.Append(AddLeadingZeroes(lineNumber.ToString().Length));
        }
            
        stringBuilder.Append(lineNumber);
        stringBuilder.Append(lineNumberAppendedText);

        stringBuilder.Append(AddTabSpacesIfNeeded(addTabSpaces, line));

        return stringBuilder.ToString();
    }
    
    public static string[] AddLineNumbers(string[] lines, int lineIncrementor, int initialLineNumber, string lineNumberAppendedText, bool assignEmptyLinesANumber, int numberOfEmptyLinesToGroupTogether, int columnNumber, bool tabSpaceAfterLineNumber, bool addLeadingZeroes, string? listNumbersWithString = null)
    {
        List<string> list = new List<string>();
        
        for(int index = 0; index < lines.Length; index++)
        {
            string line = lines[index];

            int lineNumber = CalculateLineNumber(index, lineIncrementor, initialLineNumber);
            
            if ((!assignEmptyLinesANumber && !line.Equals(string.Empty) && listNumbersWithString == null) ||
                (listNumbersWithString != null && line.Contains(listNumbersWithString)) ||
                (assignEmptyLinesANumber && line.Equals(string.Empty)))
            {
                if (line.Equals(string.Empty) && NextXLinesIsEmpty(numberOfEmptyLinesToGroupTogether, index, lines) && assignEmptyLinesANumber)
                {
                    if (numberOfEmptyLinesToGroupTogether > 1)
                    {
                        for (int emptyLine = 0; emptyLine < numberOfEmptyLinesToGroupTogether % 2; emptyLine++)
                        {
                            list.Add(string.Empty);
                        }
                   
                        list.Add(ConstructLine(lineNumber, columnNumber, line, lineNumberAppendedText,
                            tabSpaceAfterLineNumber, addLeadingZeroes));
                   
                        for (int emptyLine = 0; emptyLine < numberOfEmptyLinesToGroupTogether % 2; emptyLine++)
                        {
                            list.Add(string.Empty);
                        }
                        
                        index += numberOfEmptyLinesToGroupTogether - 1;
                    }
                    else
                    {
                        list.Add(ConstructLine(lineNumber, columnNumber, line, lineNumberAppendedText, tabSpaceAfterLineNumber, addLeadingZeroes));
                    }
                }
                
                list.Add(ConstructLine(lineNumber, columnNumber, line, lineNumberAppendedText, tabSpaceAfterLineNumber, addLeadingZeroes));
            }
        }

        return list.ToArray();
    }
}