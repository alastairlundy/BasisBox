using System;
using System.IO;
using System.Linq;
using CliUtilsLib;
using ConCat.Cli.Helpers;
using ConCat.Cli.Localizations;
using ConCat.Logic.Library;
using NLine.Library;

namespace ConCat.Cli.SubCommands;

internal class CopyingSubCommands
{
    internal static int CopySingleFile(string[] fileArguments, bool useDebugging, bool addLineNumbering)
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
                ConCatCopying.CopyFile(files!.Value.existingFiles[0], files!.Value.newFiles[0], addLineNumbering);

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
            return ConsoleHelper.HandleException(new ArgumentException(Resources.Exceptions_Copying_InvalidExistingArgs, nameof(files)), Resources.Exceptions_Copying_InvalidExistingArgs, useDebugging);
        }
    }
}