using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CliUtilsLib;

using ConCat.Cli.Helpers;
using ConCat.Cli.Localizations;
using ConCat.Library;
using ConCat.Logic.Library;
using NLine.Library;

namespace ConCat.Cli.SubCommands;

internal static class AppendingSubCommand
{
    internal static int AppendFiles(string[] fileArguments, bool useDebugging, bool useLineNumbering)
    {
        (string[] existingFiles, string[] newFiles)? files = FileArgumentFinder.GetFilesBeforeAndAfterSeparator(fileArguments, ">");
        
              try
              {
                  ConCatAppender.AppendFiles(files!.Value.existingFiles, files!.Value.newFiles, useLineNumbering);
                  
                  return 1;
              }
              catch (UnauthorizedAccessException exception)
              {
                  return ConsoleHelper.HandleException(exception,
                      Resources.Exception_Permissions_Invalid.Replace("{x}", exception.Source), useDebugging);
              }
              catch (FileNotFoundException exception)
              {
                  return ConsoleHelper.HandleException(exception,
                      Resources.Exception_FileNotFound.Replace("{x}", exception.Source), useDebugging);
              }
              catch(Exception exception)
              {
                  return ConsoleHelper.HandleException(exception,
                      Resources.Exceptions_Generic.Replace("{x}", exception.Source), useDebugging);
              }
    }
}