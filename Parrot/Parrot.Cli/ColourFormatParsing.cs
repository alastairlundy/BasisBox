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