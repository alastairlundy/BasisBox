using System;
using System.IO;
using System.Linq;
using NLine.Library;

namespace ConCat.Library.Logic;

public static class ConCatCopying
{
    public static void CopyFile(string existingFile, string newFile, bool addLineNumbering)
    {
        try
        {
            string[] newFileContents = File.ReadAllLines(existingFile);

            if (File.Exists(existingFile))
            {
                File.Delete(existingFile);
            }

            if (addLineNumbering)
            {
                newFileContents = LineNumberer.AddLineNumbers(newFileContents, ". ").ToArray();

                File.WriteAllLines(newFile, newFileContents);
            }
            else
            {
                File.Copy(existingFile, newFile);
            }
        }
        catch (UnauthorizedAccessException exception)
        {
            throw new UnauthorizedAccessException(exception.Message, exception);
        }
        catch (FileNotFoundException exception)
        {
            throw new FileNotFoundException(exception.Message, exception.FileName, exception);
        }
        catch(Exception exception)
        {
            throw new Exception(exception.Message, exception);
        }
    }
}