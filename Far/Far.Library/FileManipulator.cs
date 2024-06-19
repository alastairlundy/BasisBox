namespace Far.Library;

    internal class FileManipulator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <param name="contents"></param>
        public static void SaveFile(string path, string fileName, string[] contents)
        {
            path = Path.Combine(path, fileName);

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            File.WriteAllLines(path, contents);
        }
    }
