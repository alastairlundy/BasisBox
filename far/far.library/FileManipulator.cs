using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace far.library
{
    internal class FileManipulator
    {
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
}
