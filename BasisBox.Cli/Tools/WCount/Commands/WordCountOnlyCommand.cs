﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BasisBox.Cli.Helpers;
using BasisBox.Cli.Localizations;
using BasisBox.Cli.Tools.WCount.Settings;

using Spectre.Console;
using Spectre.Console.Cli;

using WCount.Library;

namespace BasisBox.Cli.Tools.WCount.Commands
{
    public class WordCountOnlyCommand : Command<WordCountOnlyCommand.Settings>
    {
        public class Settings : SharedWCountSettings
        {

        }

        public override int Execute(CommandContext context, Settings settings)
        {
            ExceptionFormats exceptionFormats;

            if (settings.Verbose)
            {
                exceptionFormats = ExceptionFormats.Default;
            }
            else
            {
                exceptionFormats = ExceptionFormats.NoStackTrace;
            }

            int fileResult = FileArgumentHelpers.HandleFileArgument(settings.Files, exceptionFormats);

            if(fileResult == -1)
            {
                return -1;
            }


            try
            {
                WordCounter wordCounter = new();

                ulong totalWords = 0;

                foreach (string file in settings.Files!)
                {
                    ulong wordCount = wordCounter.CountWordsInFile(file);
                    totalWords += wordCount;

                    string wordLabel = "";

                    if (wordCount == 1)
                    {
                        wordLabel = Resources.WCount_App_Labels_Words_Singular;
                    }
                    else
                    {
                        wordLabel = Resources.WCount_App_Labels_Words_Plural;
                    }

                    AnsiConsole.WriteLine($"{file} {wordCount} {wordLabel}");
                }

                if (settings.Files.Length > 1)
                {
                    if (totalWords == 0 || totalWords > 1)
                    {
                        AnsiConsole.WriteLine($"{Resources.WCount_App_Labels_Total} {totalWords} {Resources.WCount_App_Labels_Words_Plural}");
                    }
                    else
                    {
                        AnsiConsole.WriteLine($"{Resources.WCount_App_Labels_Total} {totalWords} {Resources.WCount_App_Labels_Words_Singular}");
                    }
                }

                return 0;
            }
            catch(Exception ex) 
            {
                AnsiConsole.WriteException(ex, exceptionFormats);
                return -1;
            }
        }
    }
}
