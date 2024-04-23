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
                tasks[index] = Task.Run(() =>
                {
                    newFiles.Add(files[index], UpdateFile(mode, files[index], find, replace).Value);
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
                if (existingItem.GetType().Equals(typeof(char)))
                {
                    fileContents = Replacer.ReplaceCharacter(file, existingItem, replace);
                }
            }
        }
    }
}
