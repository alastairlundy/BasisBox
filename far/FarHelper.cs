using far.library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace far
{
    internal class FarHelper
    {
        public static Dictionary<string, string[]> UpdateFiles(string mode, string[] files, string[] find, string replace)
        {
            Dictionary<string, string[]> newFiles = new Dictionary<string, string[]>();

            Task[] tasks = new Task[files.Length];

            for (int index = 0; index < files.Length; index++)
            {
                var index1 = index;
                tasks[index] = Task.Run(() =>
                {
                    newFiles.Add(files[index1], UpdateFile(mode, files[index1], find, replace).Value);
                });
            }

            Task.WaitAll(tasks);

            return newFiles;
        }

        internal static KeyValuePair<string, string[]> UpdateFile(string mode, string file, string[] find, string replace)
        {

            string[] fileContents = File.ReadAllLines(file);

            foreach (string existingItem in find)
            {
                if (char.Parse(existingItem).ToString().Equals(existingItem))
                {
                    fileContents = Replacer.ReplaceCharacter(file, char.Parse(existingItem), replace);
                }
            }
        }
    }
}
