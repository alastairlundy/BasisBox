using CliUtilsLib;

using ConCat.Library;

namespace ConCat.Logic.Library;

public static class ConCatAppender
{
    public static void AppendFiles(IEnumerable<string> existingFiles, IEnumerable<string> newFiles, bool addLineNumbering)
    {
              try
              {
                  FileAppender fileAppender = new FileAppender(addLineNumbering);
                  
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