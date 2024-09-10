/*
    BasisBox - WCount
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

using System.ComponentModel;

using System.Reflection;
using System.Text;

using AlastairLundy.Extensions.System;

using Spectre.Console;
using Spectre.Console.Cli;

using WCount.Cli.Localizations;

using WCount.Library;
using WCount.Library.Extensions;

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
                    LineCounter lineCounter = new LineCounter();
                    
                    grid.AddColumn();
                    grid.AddColumn();

                    ulong totalLines = 0;
                    
                    foreach (string file in settings.Files!)
                    {
                        ulong lineCount = lineCounter.CountLinesInFile(file);
                        totalLines += lineCount;
                        grid.AddRow(new string[] { lineCount.ToString(), new TextPath(file).ToString()!});
                    }

                    grid.AddRow(new string[] { totalLines.ToString() , Resources.App_Total_Label});
                    
                    AnsiConsole.Write(grid);
                    return 0;
                }

                if (settings.WordCount)
                {
                    WordCounter wordCounter = new WordCounter();
                    
                    grid.AddColumn();
                    grid.AddColumn();

                    ulong totalWords = 0;
                    
                    foreach (string file in settings.Files!)
                    {
                        ulong wordCount = wordCounter.CountWordsInFile(file);
                        totalWords += wordCount;
                        grid.AddRow(new string[] { wordCount.ToString(), file});
                    }

                    grid.AddRow(new string[] { totalWords.ToString(), Resources.App_Total_Label});
                    
                    AnsiConsole.Write(grid);
                    return 0;
                }

                if (settings.CharacterCount)
                {
                    CharCounter charCounter = new CharCounter();
                    
                    grid.AddColumn();
                    grid.AddColumn();

                    ulong totalChars = 0;
                    foreach (string file in settings.Files!)
                    {
                        ulong charCount = charCounter.CountCharactersInFile(file);
                        totalChars += charCount;
                        grid.AddRow(new string[] { charCount.ToString(), file });
                    }

                    grid.AddRow(new string[] {totalChars.ToString(), Resources.App_Total_Label});
                    
                    AnsiConsole.Write(grid);
                    return 0;
                }
                
                if (settings.ByteCount)
                {
                    ByteCounter byteCounter = new ByteCounter();

                    grid.AddColumn();
                    grid.AddColumn();

                    ulong totalBytes = 0;
                    
                    foreach (string file in settings.Files!)
                    {
                        ulong byteCount = byteCounter.CountBytesInFile(file, Encoding.UTF8);
                        totalBytes += byteCount;
                        grid.AddRow(new string[] { byteCount.ToString(), file});
                    }

                    grid.AddRow(new string[] { totalBytes.ToString(), Resources.App_Total_Label});

                    AnsiConsole.Write(grid);
                    return 0;
                }

                if (!settings.WordCount && !settings.LineCount && !settings.ByteCount && !settings.CharacterCount)
                {
                    WordCounter wordCounter = new WordCounter();
                    CharCounter charCounter = new CharCounter();
                    LineCounter lineCounter = new LineCounter();
                    
                    ulong totalLineCount = 0;
                    ulong totalWordCount = 0;
                    ulong totalCharCount = 0;

                    foreach (string file in settings.Files!)
                    {
                        totalLineCount += lineCounter.CountLinesInFile(file);
                        totalWordCount += wordCounter.CountWordsInFile(file);
                        totalCharCount += charCounter.CountCharactersInFile(file);
                    }
                    
                    grid.AddColumn();
                    grid.AddColumn();
                    grid.AddColumn();
                    
                    foreach (string file in settings.Files!)
                    {
                        grid.AddRow(new string[] { lineCounter.CountLinesInFile(file).ToString(), file.CountWords().ToString(), charCounter.CountCharactersInFile(file).ToString(), file});
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