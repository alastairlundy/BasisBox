/*
    BasisBox - Parrot
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

using Spectre.Console;

namespace Parrot.Cli;

public class ColourFormatParsing
{

    public static Spectre.Console.Color GetColour(string line)
    {
        Spectre.Console.Color color;
        
        
    }
    
    public static Grid Format(string[] lines)
    {
        Grid grid = new();
        
        foreach (string line in lines)
        {
            string newLine = line;

            Color? backgroundColor;
            Color? foregroundColor;
            
            if (line.Contains("${") && line.Contains('}'))
            {
                newLine = newLine.Replace("$", string.Empty);
                
                if (newLine.Contains("BG"))
                {
                    newLine = newLine.Replace("BG", string.Empty);

                    backgroundColor = GetColour(newLine);
                }

                if (newLine.Contains("FG"))
                {
                    newLine = newLine.Replace("FG", string.Empty);

                    foregroundColor = GetColour(newLine);
                }
            }
        }

        return grid;
    }
}