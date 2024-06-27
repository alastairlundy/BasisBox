using CliUtilsLib;

using ConCat.Library;

namespace ConCat.Logic.Library;

public static class ConCatAppender
{
    public static void AppendFiles(string[] existingFiles, string[] newFiles, bool useLineNumbering)
    {
              try
              {
                  FileAppender fileAppender = new FileAppender(useLineNumbering);
                  
                  fileAppender.AppendFiles(existingFiles);
                  
                  foreach (string file in newFiles)
                  {
                      fileAppender.WriteToFile(file);
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