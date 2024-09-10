/*

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
using CliUtilsLib;

using ConCat.Cli.Helpers;
using ConCat.Cli.Localizations;
using ConCat.Cli.Settings;
using ConCat.Library.Logic;
using Spectre.Console;
using Spectre.Console.Cli;

namespace ConCat.Cli.Commands;

public class CopyCommand : Command<CopyCommand.Settings>
{
   public class Settings : BasicConCatSettings
   {
      
   }
   
   public override int Execute(CommandContext context, Settings settings)
   {
      if (settings.Files == null || settings.Files.Any())
      {
         AnsiConsole.WriteException(new NullReferenceException(Resources.Exceptions_NoFileProvided));
         return -1;
      }
        
      (IEnumerable<string> existingFiles, IEnumerable<string> newFiles)? files = FileArgumentFinder.GetFilesBeforeAndAfterSeparator(settings.Files, ">");

      try
      {
         ConCatCopying.CopyFile(files!.Value.existingFiles.First(), files!.Value.newFiles.First(), settings.AppendLineNumbers);

         AnsiConsole.WriteLine(Resources.Command_Copy_Success.Replace("{x}", files.Value.existingFiles.First()).Replace("{y}", files.Value.newFiles.First()));

         return 1;
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