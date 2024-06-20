using System;
using System.IO;
using CliUtilsLib;
using ConCat.Cli.Helpers;
using ConCat.Cli.Localizations;

namespace ConCat.Cli.SubCommands;

internal class CopyingSubCommands
{
    internal static int CopySingleFile(string[] fileArguments, bool useDebugging, bool useLineNumbering)
    {
        (string[] existingFiles, string[] newFiles)? files = FileArgumentFinder.GetFilesBeforeAndAfterSeparator(fileArguments, ">>");

        if (files == null)
        {
            return ConsoleHelper.HandleException(new NullReferenceException(Resources.Exceptions_NoFileProvided), string.Empty, useDebugging);
        }
        
        if (files.Value.existingFiles.Length == 1 && files.Value.newFiles.Length == 1)
        {
            try
            {
                string[] newFileContents = File.ReadAllLines(files.Value.existingFiles[0]);
                
                if (File.Exists(files.Value.newFiles[0]))
                {
                    File.Delete(files.Value.newFiles[0]);
                }
                
                if (useLineNumbering)
                {
                    newFileContents = ConsoleHelper.AddLineNumbering(newFileContents);
                     
                     File.WriteAllLines(files.Value.newFiles[0], newFileContents);
                }
                else
                {
                    File.Copy(files.Value.existingFiles[0], files.Value.newFiles[0]);
                }

                return 0;
            }
            catch (UnauthorizedAccessException exception)
            {
                return ConsoleHelper.HandleException(exception,
                    Resources.Exception_Permissions_Invalid.Replace("{x}", exception.Source), useDebugging);
            }
            catch(Exception exception)
            {
                return ConsoleHelper.HandleException(exception, Resources.Exceptions_Generic.Replace("{x}", exception.Message), useDebugging);
            }
        }
        else
        {
            return ConsoleHelper.HandleException(new ArgumentException())
        }
    }

    internal static int CopyFiles(string[] fileArguments, bool useDebugging)
    {
        (string[] existingFiles, string[] newFiles)? files = FileArgumentFinder.GetFilesBeforeAndAfterSeparator(fileArguments, ">>");

        
    }
}