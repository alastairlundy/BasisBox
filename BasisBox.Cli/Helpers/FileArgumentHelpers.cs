using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BasisBox.Cli.Localizations;
using Spectre.Console;

namespace BasisBox.Cli.Helpers
{
    internal static class FileArgumentHelpers
    {
        internal static int HandleFileArgument(string? file, ExceptionFormats formats)
        {
            if(file == null)
            {
                AnsiConsole.WriteException(new ArgumentException(Resources.Exceptions_NoFileProvided), formats);
                return -1;
            }

            return 0;
        }

        internal static int HandleFileArgument(string[]? files, ExceptionFormats formats)
        {
           if(files == null)
            {
                AnsiConsole.WriteException(new NullReferenceException(Resources.Exceptions_NoFileProvided), formats);
                return -1;
            }
            else if(files.Length == 0)
            {
                AnsiConsole.WriteException(new ArgumentException(Resources.Exceptions_NoFileProvided));
                return -1;
            }

            return 0;
        }
    }
}
