namespace NewDir.Library;

public static class NewDirectory
{
    public static void Create(string directoryPath, UnixFileMode unixCreateMode, bool createParentPaths)
    {
        if (createParentPaths)
        {
            string[] directories = Directory.GetDirectories(directoryPath);

            foreach (string directory in directories)
            {
                if (!Directory.Exists(directory))
                {
                    if (OperatingSystem.IsWindows())
                    {
                        Directory.CreateDirectory(directory);
                    }
                    else
                    {
                        Directory.CreateDirectory(directory, unixCreateMode);
                    }
                }
            }
        }
        else
        {
            if (OperatingSystem.IsWindows())
            {
                Directory.CreateDirectory(directoryPath);
            }
            else
            {
                Directory.CreateDirectory(directoryPath, unixCreateMode);
            }
        }
    }
}