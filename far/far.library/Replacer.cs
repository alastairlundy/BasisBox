namespace far.library;

public class Replacer
{
    public static string[] ReplaceCharacter(string filePath, char existingChar, char replacementChar)
    {
        string[] lines = File.ReadAllLines(filePath);

        foreach (string line in lines)
        {
            string[] words = line.Split(' ');

            for(int index = 0; index < words.Length; index++)
            {
                for(int characters = 0; characters < words[index].Length; characters++)
                {
                    if (words[index][characters].Equals(existingChar))
                    {
                        words[index] = words[index].Replace(existingChar, replacementChar);
                    }   
                }
            }
        }

        return lines;
    }

    public static string[] ReplaceExactMatch(string filePath, string existingString, string replacementString)
    {
        string[] lines = File.ReadAllLines(filePath);

        foreach (string line in lines)
        {
            string[] words = line.Split(' ');

            for (int index = 0; index < words.Length; index++)
            {
                if (words[index].Equals(existingString)){
                    words[index] = replacementString;
                }
            }
        }

        return lines;
    }

    public static string[] ReplacePartialMatch(string filePath, string existingString, string replacementString)
    {
        string[] lines = File.ReadAllLines(filePath);

        foreach (string line in lines)
        {
            string[] words = line.Split(' ');

            for (int index = 0; index < words.Length; index++)
            {
                if (words[index].Contains(existingString))
                {
                    words[index] = words[index].Replace(existingString, replacementString);
                }
            }
        }

        return lines;
    }
}