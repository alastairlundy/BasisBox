using Del.Library.Localizations;

namespace Del.Library.Extensions;

public static class IsEmptyExtensions
{
    
    public static bool IsDirectoryEmpty(this string directory)
    {
        if (Directory.Exists(directory))
        {
            return Directory.GetDirectories(directory).Length == 0 && Directory.GetFiles(directory).Length == 0;
        }
        else
        {
            throw new DirectoryNotFoundException($"{Resources.Exceptions_DirectoryNotFound.Replace("{x}", directory)}");
        }
    }
}