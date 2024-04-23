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


using McMaster.Extensions.CommandLineUtils;
using Sarcasm;

        CommandLineApplication app = new CommandLineApplication();
        app.HelpOption("-h|--help");
        app.VersionOption("-v|--version", app.GetFullNameAndVersion);
        
       var license = app.Option("--license", "Displays the project's license", CommandOptionType.NoValue);
       var output = app.Option("-o|--output", "The path to output results to.", CommandOptionType.SingleOrNoValue);
       var input = app.Option("-i|--input", "The path to input text files.", CommandOptionType.SingleOrNoValue);


app.OnExecute(() =>
{
    if (license.HasValue())
    {
        ConsoleHelper.PrintLicenseToConsole();
    }

    string[] textToBeConverted;

    if (input.HasValue())
    {
        textToBeConverted = File.ReadAllLines(input.Value()!);
    }
    else
    {
        textToBeConverted = app.RemainingArguments.ToArray();
    }
    
    string[] results = TextProcessor.ToSarcasmText(textToBeConverted);

    if (output.HasValue())
    {

        string outputString = output.Value()!;

        if (outputString.EndsWith(".txt") || outputString.EndsWith(".rtf"))
        {
            TextProcessor.SaveToFile(outputString, results);
        }
    }
    else
    {
        ConsoleHelper.PrintResults(results);
    }
});

return app.Execute(args);