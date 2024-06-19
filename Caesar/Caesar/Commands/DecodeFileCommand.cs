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

using System.Collections.Generic;
using System.IO;

using Caesar.Library;

using Spectre.Console;
using Spectre.Console.Cli;

namespace Caesar.Commands;

public class DecodeFileCommand : Command<DecodeFileCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<Files>")]
        public string? File { get; init; }
        
        [CommandOption("-o|--output:<file>")]
        public string? OutputFile { get; init; }
            
        [CommandOption("-s|--shift:<number_to_shift_by>")]
        public int? ShiftAmount { get; init; }
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        if (settings.File == null)
        {
            AnsiConsole.WriteException(new FileNotFoundException());
            return -1;
        }
        
        int shift = ConsoleHelper.ShiftHandler(settings.ShiftAmount);

        CaesarCipher caesarCipher = new CaesarCipher();

        List<string> newValues = new List<string>();
        
        string[] lines = File.ReadAllLines(settings.File);

        foreach (string line in lines)
        {
            string[] newWords = caesarCipher.Decode(File.ReadAllLines(line), shift);

            foreach (string word in newWords)
            {
                newValues.Add(word);
            }
        }

        if (settings.OutputFile == null)
        {
            ConsoleHelper.SaveResultsToFile(settings.File, newValues.ToArray());
        }
        else
        {
            ConsoleHelper.SaveResultsToFile(settings.OutputFile!, newValues.ToArray());
        }

       
        return 0;
    }
}