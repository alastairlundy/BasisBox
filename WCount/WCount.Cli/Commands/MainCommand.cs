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

using System.ComponentModel;

using System.Reflection;

using AlastairLundy.Extensions.System;

using Spectre.Console;
using Spectre.Console.Cli;

using WCount.Cli.Localizations;
using WCount.Library;

namespace WCount.Cli.Commands;

public class MainCommand : Command<MainCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(1, "<files>")]
        public string[]? Files { get; init; }
        
        [CommandOption("-l|--line-count")]
        [DefaultValue(false)]
        public bool LineCount { get; init; }
        
        [CommandOption("-w|--word-count")]
        [DefaultValue(false)]
        public bool WordCount { get; init; }
        
        [CommandOption("-m|--character-count")]
        [DefaultValue(false)]
        public bool CharacterCount { get; init; }
        
        [CommandOption("-c|--byte-count")]
        [DefaultValue(false)]
        public bool ByteCount { get; init; }
        
        [CommandOption("-v|--version")]
        [DefaultValue(false)]
        public bool Version { get; init; }
        
        [CommandOption("--license")]
        [DefaultValue(false)]
        public bool ShowLicense { get; init; }
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        if (settings.Files == null || settings.Files!.Length < 1)
        {
            AnsiConsole.WriteException(new FileNotFoundException());
            return -1;
        }
        
        if (settings.Version)
        {
            AnsiConsole.WriteLine($"v{Assembly.GetExecutingAssembly().GetName().Version.ToFriendlyVersionString()}");
            return 0;
        }

        if (settings.ShowLicense)
        {
            foreach (string line in File.ReadLines($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}NOTICE.txt"))
            {
                AnsiConsole.WriteLine(line);
            }
            
            AnsiConsole.WriteLine();
            return 0;
        }
        
        try
        {
            Grid grid = new();

                if (settings.LineCount)
                {
                    grid.AddColumn();
                    grid.AddColumn();
                    
                    foreach (string file in settings.Files!)
                    {
                        grid.AddRow(new string[] { LineCounter.CountLinesInt(file).ToString(), new TextPath(file).ToString()!});
                    }

                    grid.AddRow(new string[] { LineCounter.TotalLineCount(settings.Files).ToString(), Resources.App_Total_Label});
                    
                    AnsiConsole.Write(grid);
                    return 0;
                }

                if (settings.WordCount)
                {
                    grid.AddColumn();
                    grid.AddColumn();
                    
                    foreach (string file in settings.Files!)
                    {
                        grid.AddRow(new string[] { WordCounter.CountWords(file).ToString(), file});
                    }

                    grid.AddRow(new string[] { WordCounter.TotalWordCount(settings.Files).ToString(), Resources.App_Total_Label});
                    
                    AnsiConsole.Write(grid);
                    return 0;
                }

                if (settings.CharacterCount)
                {   
                    grid.AddColumn();
                    grid.AddColumn();
                    
                    foreach (string file in settings.Files!)
                    {
                        grid.AddRow(new string[] { CharCounter.CountCharacters(file).ToString(), file });
                    }

                    grid.AddRow(new string[] { CharCounter.TotalCharCount(settings.Files).ToString(), Resources.App_Total_Label});
                    
                    AnsiConsole.Write(grid);
                    return 0;
                }
                
                if (settings.ByteCount)
                {
                    grid.AddColumn();
                    grid.AddColumn();
                    
                    foreach (string file in settings.Files!)
                    {
                        grid.AddRow(new string[] { ByteCounter.CountBytes(file).ToString(), file});
                    }

                    grid.AddRow(new string[] { ByteCounter.TotalByteCount(settings.Files).ToString(), Resources.App_Total_Label});

                    AnsiConsole.Write(grid);
                    return 0;
                }

                if (!settings.WordCount && !settings.LineCount && !settings.ByteCount && !settings.CharacterCount)
                {
                    ulong totalLineCount = LineCounter.TotalLineCount(settings.Files!);
                    ulong totalWordCount = WordCounter.TotalWordCount(settings.Files!);
                    ulong totalCharCount = CharCounter.TotalCharCount(settings.Files!);
                    
                    grid.AddColumn();
                    grid.AddColumn();
                    grid.AddColumn();
                    
                    foreach (string file in settings.Files!)
                    {
                        grid.AddRow(new string[] { LineCounter.CountLinesInt(file).ToString(), WordCounter.CountWords(file).ToString(), CharCounter.CountCharacters(file).ToString(), file});
                    }

                    grid.AddRow(new string[] { totalLineCount.ToString(), totalWordCount.ToString(), totalCharCount.ToString(), Resources.App_Total_Label});
                    
                    AnsiConsole.Write(grid);
                    return 0;
                }
        }
        catch(Exception exception)
        {
            AnsiConsole.WriteException(exception);
            return -1;
        }

        return -1;
    }
}