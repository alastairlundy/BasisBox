// See https://aka.ms/new-console-template for more information

using far;
using far.library;
using McMaster.Extensions.CommandLineUtils;

internal class Program
{
    private static void Main(string[] args)
    {
        CommandLineApplication app = new CommandLineApplication();
        app.HelpOption("-h | --help");
        app.VersionOption("-v | --version", app.GetFullNameAndVersion);

        var license = app.Option("--license", "Displays the project's license", CommandOptionType.NoValue);

        app.Command("replace", replaceCommand =>
        {
            var input = replaceCommand.Option("--input|-i:<INPUT_FILE_OR_DIRECTORY>", "The directory or file(s) to be searched.", CommandOptionType.SingleValue).Accepts(config => 
            config.LegalFilePath().ExistingFileOrDirectory()).IsRequired();

            var find = replaceCommand.Argument("<existing_string>", "The string to look for.", false);
            var replace = replaceCommand.Argument("<replacement_string>", "", false);


            app.Command("new", newCommand =>
            {
                newCommand.Description = "Replaces the specified strings but creates new files instead of overwriting the existing files. ";

                var output = newCommand.Option("--output|-o:<OUTPUT_FILE_OR_DIRECTORY>", "", CommandOptionType.SingleValue);


            });

        });

        app.Command("contains", containsCommand =>
        {

        });

        app.OnExecute(() =>
        {
            if (license.HasValue())
            {
                ConsoleHelper.PrintLicenseToConsole();
            }

            Dictionary<string, string[]> pairs = new Dictionary<string, string[]>();

            if (source.HasValue() && find.HasValue && replace.HasValue())
            {
                if (mode.HasValue())
                {
                    Dictionary<string, string[]> newFiles = FarHelper.UpdateFiles(mode.Value()!, source.Values.ToArray()!, find.Values.ToArray()!, replace.Value()!);
                }
                else
                {
                    Dictionary<string, string[]> newFiles = FarHelper.UpdateFiles(source.Values.ToArray(), find.Values.ToArray(), replace.Value());
                }
            }
        });
    }


}