using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BasisBox.Cli.Helpers;
using BasisBox.Cli.Localizations;
using BasisBox.Cli.Tools.WCount.Settings;

using Spectre.Console;
using Spectre.Console.Cli;

namespace BasisBox.Cli.Tools.WCount.Commands
{
    public class WordCountOnlyCommand : Command<WordCountOnlyCommand.Settings>
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

            if(fileResult == -1)
            {
                return -1;
            }


            throw new NotImplementedException();
        }
    }
}
