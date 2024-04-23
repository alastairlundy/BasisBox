using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace far
{
    internal class ConsoleHelper
    {
        public static void PrintLicenseToConsole()
        {
            string[] licenseStrings =
                File.ReadAllLines(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "LICENSE.txt");

            foreach (string str in licenseStrings)
            {
                Console.WriteLine(str);
            }

            Console.WriteLine();
        }
    }
}
