using Del.Library.Localizations;

namespace Del.Library;

public class DirectoryEliminator
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="directory"></param>
    /// <param name="deleteEmptyDirectory"></param>
    /// <exception cref="DirectoryNotFoundException"></exception>
    public static void DeleteRecursively(string directory, bool deleteEmptyDirectory)
    {
        if (Directory.Exists(directory))
        {
            if (Directory.GetDirectories(directory).Length > 0)
            {
                foreach (string subDirectory in Directory.GetDirectories(directory))
                {
                    if (Directory.GetFiles(subDirectory).Length > 0)
                    {
                        foreach (string file in Directory.GetFiles(subDirectory))
                        {
                            File.Delete(file);
                        }
                    }

                    int numberOfFiles = Directory.GetFiles(directory).Length;

                    if (deleteEmptyDirectory == true && numberOfFiles == 0 || numberOfFiles > 0)
                    {
                        Directory.Delete(subDirectory);
                    }
                }
            }
            else
            {
                if (deleteEmptyDirectory)
                {
                    Directory.Delete(directory);
                }
            }
        }

        throw new DirectoryNotFoundException(Resources.Exceptions_DirectoryNotFound.Replace("{x}", directory));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="directory"></param>
    /// <param name="deleteEmptyDirectories"></param>
    /// <returns></returns>
    public static bool TryDeleteRecursively(string directory, bool deleteEmptyDirectories)
    {
        try
        {
            DeleteRecursively(directory, deleteEmptyDirectories);
            return true;
        }
        catch
        {
            return false;
        }
    }
}