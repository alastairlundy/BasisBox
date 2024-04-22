using AlastairLundy.System.Extensions.StringExtensions;

namespace Sarcasm;

public class TextProcessor
{
    public static void SaveToFile(string pathToFile, string[] results)
    {
        File.WriteAllLines(pathToFile, results);
    }
    
    public static string[] ToSarcasmText(string[] textToBeConverted)
    {
        string[] results = new string[textToBeConverted.Length];
        
        for(int index = 0; index < textToBeConverted.Length; index++)
        {
            string[] wordsInStr = textToBeConverted[index].Split(" ");

            results[index] = "";
            
            foreach (string str in wordsInStr)
            {
                results[index] += str.ToSarcasmText();
            }
        }

        return results;
    }
}