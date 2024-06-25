using System;
using System.IO;

using CliUtilsLib;

using ConCat.Cli.Helpers;
using ConCat.Cli.Localizations;
using ConCat.Library;

namespace ConCat.Cli.SubCommands;

internal static class AppendingSubCommand
{
    internal static int AppendFiles(string[] fileArguments, bool useDebugging, bool useLineNumbering)
    {
        (string[] existingFiles, string[] newFiles)? files = FileArgumentFinder.GetFilesBeforeAndAfterSeparator(fileArguments, ">");

        FileAppender fileAppender = new FileAppender();
            
          foreach (string file in files!.Value.existingFiles)
          {
              try
              {
                  if (useLineNumbering)
                  {
                      fileAppender.AppendFileContents(ConsoleHelper.AddLineNumbering(File.ReadAllLines(file)));
                  }
                  else
                  {
                      fileAppender.AppendFileContents(file);
                  }
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

          foreach (string newFile in files.Value.newFiles)
          {
              fileAppender.WriteToFile(newFile);
          }
        
          return 1;
    }
}