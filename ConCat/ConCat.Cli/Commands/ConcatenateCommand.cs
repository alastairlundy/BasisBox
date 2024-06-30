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
using System.IO;
using CliUtilsLib;
using ConCat.Cli.Helpers;
using ConCat.Cli.Localizations;
using ConCat.Cli.Settings;

using Spectre.Console;
using Spectre.Console.Cli;

namespace ConCat.Cli.Commands;

public class ConcatenateCommand : Command<ConcatenateCommand.Settings>
{
    public class  Settings : BasicConCatSettings
    {
        
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        if (settings.Files == null || settings.Files.Length == 0)
        {
            AnsiConsole.WriteException(new NullReferenceException(Resources.Exceptions_NoFileProvided));
            return -1;
        }

        (string[] existingFiles, string[] newFiles)? files = FileArgumentFinder.GetFilesBeforeAndAfterSeparator(settings.Files, ">");
        
        try
        {
            
        }
        catch (UnauthorizedAccessException exception)
        {
            return ConsoleHelper.HandleException(exception,
                Resources.Exception_Permissions_Invalid.Replace("{x}", exception.Source), settings.ShowErrors);
        }
        catch (FileNotFoundException exception)
        {
            return ConsoleHelper.HandleException(exception,
                Resources.Exception_FileNotFound.Replace("{x}", exception.Source), settings.ShowErrors);
        }
        catch (Exception exception)
        {
            return ConsoleHelper.HandleException(exception,
                Resources.Exceptions_Generic.Replace("{x}", exception.Source), settings.ShowErrors);
        }
    }
}