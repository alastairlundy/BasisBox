using System;
using System.IO;
using System.Linq;

using AlastairLundy.Extensions.System.Collections;

using CliUtilsLib;

using ConCat.Cli.Helpers;
using ConCat.Cli.Localizations;

using Spectre.Console;

namespace ConCat.Cli.SubCommands;

internal static class AppendingSubCommand
{
    internal static int AppendFiles(string[] fileArguments, bool useDebugging, bool useLineNumbering)
    {
        (string[] existingFiles, string[] newFiles)? files = FileArgumentFinder.GetFilesBeforeAndAfterSeparator(fileArguments, ">");

            string[] newFileContents = [];  
            
          foreach (string file in files!.Value.existingFiles)
          {
              if (FileFinder.IsAFile(file) && File.Exists(file))
              {
                  try
                  {
                      string[] fileContents = File.ReadAllLines(file);

                      newFileContents = newFileContents.Combine(fileContents).ToArray();
                  }
                  catch(UnauthorizedAccessException exception)
                  {
                      return ConsoleHelper.HandleException(exception, Resources.Exception_Permissions_Invalid.Replace("{x}", file),
                          useDebugging);
                  }
              }
              else if(!File.Exists(file))
              {
                  return ConsoleHelper.HandleException(new FileNotFoundException(Resources.Exception_FileNotFound, file),
                      string.Empty, useDebugging);
              }
          }

          if (useLineNumbering)
          {
              newFileContents = ConsoleHelper.AddLineNumbering(newFileContents);
          }
          
          foreach (string file in files!.Value.newFiles)
          {
              if (FileFinder.IsAFile(file))
              {
                  try
                  {
                      if (File.Exists(file))
                      {
                          File.Delete(file);
                      }
                      
                      File.WriteAllLines(file, newFileContents);
                      
                      AnsiConsole.WriteLine(Resources.Command_NewFile_Success.Replace("{x}", file));
                  }
                  catch (UnauthorizedAccessException exception)
                  {
                      return ConsoleHelper.HandleException(exception, Resources.Exception_Permissions_Invalid.Replace("{x}", file),
                          useDebugging);
                  }
                  catch (Exception exception)
                  {
                      return ConsoleHelper.HandleException(exception, Resources.Exceptions_Generic.Replace("{x}", exception.Message), useDebugging);
                  }
              }
          }

          return 1;
    }
}