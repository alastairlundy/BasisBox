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

using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using AlastairLundy.Extensions.System.Collections;
using CliUtilsLib;
using ConCat.Cli.Helpers;
using ConCat.Cli.Localizations;
using ConCat.Cli.SubCommands;
using Spectre.Console;
using Spectre.Console.Cli;

namespace ConCat.Cli.Commands;

public class ConcatenateCommand : Command<ConcatenateCommand.Settings>
{
    public class Settings : CommandSettings{

        [CommandArgument(0, "<File(s)>")]
        public string[]? Files { get; init; }

        [CommandOption("-n")]
        [DefaultValue(false)]
        public bool AppendLineNumbers { get; init; }
        
        [CommandOption("--verbose|--debug")]
        [DefaultValue(false)]
        public bool ShowErrors { get; init; }
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        if(settings.Files == null || settings.Files.Length == 0)
        {
            AnsiConsole.WriteException(new NullReferenceException(Resources.Exceptions_NoFileProvided));
            return -1;
        }

        if(settings.Files.Length == 1)
        {
            if (settings.Files[0].StartsWith('>'))
            {
                string[] strings =
                    AnsiConsole.Prompt(new TextPrompt<string[]>(Resources.Command_NewFile_Prompt).AllowEmpty());

                
            }
            else
            {
                try
                {
                    ConsoleHelper.PrintLines(File.ReadAllLines(settings.Files[0]), settings.AppendLineNumbers!);
                    return 0;
                }
                catch(Exception exception) 
                {
                    AnsiConsole.WriteException(exception, ExceptionFormats.NoStackTrace);
                    return -1;
                }
            }
        }

        if (context.Arguments.Contains(">>"))
        {
            if (settings.Files.Length > 1)
            {
                (string[] existingFiles, string[] newFiles)? files = FileArgumentFinder.GetFilesBeforeAndAfterSeparator(settings.Files, ">");

                string[] newFileContents = []; 
            }
            
        }
        else if(context.Arguments.Contains(">"))
        {
            if (settings.Files.Length > 1)
            {
                return AppendingSubCommand.AppendFiles(settings.Files, settings.ShowErrors);
            }
            else
            {
                return CopyingSubCommands.CopySingleFile(settings.Files, settings.ShowErrors);
            }
        }
        
        
        throw new NotImplementedException();
    }
}