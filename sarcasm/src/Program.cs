  /*
        MIT License

        Copyright (c) 2024 Alastair Lundy

        Permission is hereby granted, free of charge, to any person obtaining a copy
        of this software and associated documentation files (the "Software"), to deal
        in the Software without restriction, including without limitation the rights
        to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
        copies of the Software, and to permit persons to whom the Software is
        furnished to do so, subject to the following conditions:

        The above copyright notice and this permission notice shall be included in all
        copies or substantial portions of the Software.

        THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
        IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
        FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
        AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
        LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
        OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
        SOFTWARE.

   */
      // See https://aka.ms/new-console-template for more information

      using System.Globalization;
      using AlastairLundy.System.Extensions.StringArrayExtensions;
        
        using AlastairLundy.System.Extensions.StringExtensions;
        using CommandLineArgsParser;

        ArgParser argumentGrammar = new ArgParser();
                
        argumentGrammar.AddFlag("help", isMandatory:false, "h", "Displays CLI usage information and help messages", null, null);
        argumentGrammar.AddFlag("version",isMandatory: false, "v", "Displays the CLI tool name and version", null, null);
        argumentGrammar.AddFlag("license", isMandatory:false, "w", "Displays the project's license", null, null);
                
        argumentGrammar.AddOption("source", isMandatory:false, "s", "The source text to be converted.", null, null, null);
        argumentGrammar.AddOption("output", isMandatory:false, "o", "Where to output the results to.", "cli", null, null);

        ArgResults argResults = argumentGrammar.Parse(args);

        if (argResults.WasParsed("help"))
        {
            //CliHelper.PrintHelpMessages(argumentGrammar);
        }

        if (argResults.WasParsed("version"))
        {
            CliHelper.PrintVersion();
            Console.WriteLine();
        }

        if (argResults.WasParsed("license"))
        {
            string[] licenseStrings =
                File.ReadAllLines(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "LICENSE.txt");

            foreach (string str in licenseStrings)
            {
                Console.WriteLine(str);
            }

            Console.WriteLine();
        }

        string[] textToBeConverted;

        if (argResults.WasParsed("source") && argResults.GetOption("source").Values != null)
        {
            textToBeConverted = argResults.GetOption("source").Values!;
        }
        else
        {
            textToBeConverted = argResults.Rest;
        }

        string[] results = new string[textToBeConverted.Length];


        for(int index = 0; index < textToBeConverted.Length; index++)
        {
            var wordsInStr = textToBeConverted[index].Split(" ");

            results[index] = "";
            
            for(int index2 = 0; index2 < wordsInStr.Length; index2++)
            {
               results[index] += wordsInStr[index2].ToSarcasmText();
            }
        }
        
        if (argResults.WasParsed("output"))
        {
            Option output = argResults.GetOption("output");

            if (output.Values != null)
            {
                if (output.Values.Contains("cli"))
                {
                    foreach (string str in results)
                    {
                        Console.WriteLine(str);
                    }
                }
                else
                {
                    if (output.Values.Length == 1)
                    {
                        string outputString = output.Values.ToString()!;

                        if (outputString.EndsWith(".txt") || outputString.EndsWith(".rtf"))
                        {
                            File.WriteAllLines(outputString, results);
                        }
                    }
                }
            }
        }