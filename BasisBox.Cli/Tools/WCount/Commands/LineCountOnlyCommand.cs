using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BasisBox.Cli.Helpers;
using BasisBox.Cli.Localizations;
using BasisBox.Cli.Tools.WCount.Settings;

using Spectre.Console;
using Spectre.Console.Cli;

using WCount.Library;

namespace BasisBox.Cli.Tools.WCount.Commands
{
    public class LineCountOnlyCommand : Command<LineCountOnlyCommand.Settings>
    {
        public class Settings : SharedWCountSettings
        {

        }

        public override int Execute(CommandContext context, Settings settings)
        {
            ExceptionFormats exceptionFormats;

            if (settings.Verbose)
            {
                exceptionFormats = ExceptionFormats.Default;
            }
            else
            {
                exceptionFormats = ExceptionFormats.NoStackTrace;
            }

            int fileResult = FileArgumentHelpers.HandleFileArgument(settings.Files, exceptionFormats);

            if (fileResult == -1)
            {
                return -1;
            }


            try
            {
                LineCounter lineCounter = new();

                int totalLines = 0;

                foreach (string file in settings.Files!)
                {
                    int lineCount = lineCounter.CountLinesInFile(file);
                    totalLines += lineCount;

                    string label = "";

                    if (lineCount == 1)
                    {
                        label = Resources.WCount_App_Labels_Lines_Singular;
                    }
                    else
                    {
                        label = Resources.WCount_App_Labels_Lines_Plural;
                    }

                    AnsiConsole.WriteLine($"{file} {lineCount} {label}");
                }

                if (settings.Files.Length > 1)
                {
                    if (totalLines == 0 || totalLines > 1)
                    {
                        AnsiConsole.WriteLine($"{Resources.WCount_App_Labels_Total} {totalLines} {Resources.WCount_App_Labels_Lines_Plural}");
                    }
                    else
                    {
                        AnsiConsole.WriteLine($"{Resources.WCount_App_Labels_Total} {totalLines} {Resources.WCount_App_Labels_Lines_Singular}");
                    }
                }

                return 0;
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex, exceptionFormats);
                return -1;
            }
        }
    }
}
