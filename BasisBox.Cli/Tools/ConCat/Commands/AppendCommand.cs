﻿/*

    BasisBox - ConCat
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AlastairLundy.Extensions.IO.Files.Concatenation;
using BasisBox.Cli.Localizations;
using BasisBox.Cli.Tools.ConCat.Helpers;
using BasisBox.Cli.Tools.ConCat.Settings;
using CliUtilsLib;
using NLine.Library;
using Spectre.Console;
using Spectre.Console.Cli;

namespace BasisBox.Cli.Tools.ConCat.Commands;

public class AppendCommand : Command<AppendCommand.Settings>
{
    public class Settings : BasicConCatSettings
    {
        
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        if (settings.Files == null || settings.Files.Any() == false)
        {
            AnsiConsole.WriteException(new NullReferenceException(Resources.Exceptions_NoFileProvided));
            return -1;
        }
        
        (IEnumerable<string> existingFiles, IEnumerable<string> newFiles)? files = FileArgumentFinder.GetFilesBeforeAndAfterSeparator(settings.Files, ">>");
        
        try
        {
            if (files == null)
            {
                AnsiConsole.WriteException(new NullReferenceException(Resources.Exceptions_NoFileProvided));
                return -1;
            }

            LineNumberer lineNumberer = new();
            
            FileAppender fileAppender = new();
            fileAppender.AppendFiles(files.Value.existingFiles);
            
            foreach (string file in files.Value.newFiles)
            {
                if (settings.AppendLineNumbers)
                {
                   IEnumerable<string> contents = fileAppender.ToEnumerable();
                   contents = lineNumberer.AddLineNumbers(contents, ". ");
                   
                   File.WriteAllLines(file, contents);
                }
                else
                {
                    fileAppender.WriteToFile(file);
                }
                
                AnsiConsole.WriteLine(Resources.ConCat_App_Commands_UpdateFile_Success.Replace("{x}", file));
            }

            return 1;
        }
        catch (UnauthorizedAccessException exception)
        {
            return ConCatConsoleHelper.HandleException(exception,
                Resources.Exceptions_Permissions_Invalid.Replace("{x}", exception.Source), settings.ShowErrors);
        }
        catch (FileNotFoundException exception)
        {
            return ConCatConsoleHelper.HandleException(exception,
                Resources.Exceptions_FileNotFound.Replace("{x}", exception.Source), settings.ShowErrors);
        }
        catch (Exception exception)
        {
            return ConCatConsoleHelper.HandleException(exception,
                Resources.Exceptions_Generic.Replace("{x}", exception.Source), settings.ShowErrors);
        }
    }
}