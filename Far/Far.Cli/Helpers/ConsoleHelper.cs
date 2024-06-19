namespace Far.Cli.Helpers
{
    internal class ConsoleHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public static void PrintLicenseToConsole()
        {
            string[] licenseStrings =
                File.ReadAllLines($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}LICENSE.txt");

            foreach (string str in licenseStrings)
            {
                Console.WriteLine(str);
            }

            Console.WriteLine();
        }
    }
}
