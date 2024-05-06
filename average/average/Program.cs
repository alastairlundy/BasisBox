using AlastairLundy.Extensions.System.VersionExtensions;
using AlastairLundy.Extensions.System.VersionExtensions.Enums;

using AlastairLundy.Extensions.System;

using McMaster.Extensions.CommandLineUtils;
using System.Reflection;

using AlastairLundy.Extensions.System.AssemblyExtensions;
using average.localizations;
using average.Helpers;
using System.ComponentModel.Design;

namespace average;

internal class Program
{
    static int Main(string[] args)
    {
        CommandLineApplication app = new CommandLineApplication();

        var appVersion = Assembly.GetExecutingAssembly().GetProjectVersion();

        var help = app.HelpOption("-h|--help");
        var license = app.Option("-l|--license", Resources.Command_License_Description, CommandOptionType.NoValue);
        var version = app.VersionOption("-v|--version", appVersion.GetFriendlyVersionToString(), appVersion.ToString());

        app.Command("mean", meanCommand =>
        {
            var geometricFlag = meanCommand.Option("--geometric", Resources.Command_GeometricMean_Description, CommandOptionType.SingleOrNoValue);
            var arithmeticFlag = meanCommand.Option("-arithmetic", Resources.Command_ArithmeticMean_Description, CommandOptionType.SingleOrNoValue);

            var decimalRounding = meanCommand.Option("-dp | --decimal-places", Resources.Rounding_DecimalPlaces_Description, CommandOptionType.SingleValue).DefaultValue = "2";

            var numbers = meanCommand.Argument("<numbers>", Resources.Numbers_Description, true);


            if (geometricFlag.HasValue())
            {

            }
            else if (arithmeticFlag.HasValue())
            {

            }

        });

        app.Command("median", medianCommand =>
        {

        });

        app.Command("mode", modeCommand =>
        {
            var modeHelp = modeCommand.HelpOption("-h|--help");

            modeCommand.Description = Resources.Command_Mode_About;

            var numbers = modeCommand.Argument("<numbers>", Resources.Numbers_Description, true);


            modeCommand.OnExecute(() =>
            {



                if (modeHelp.HasValue())
                {
                    modeCommand.ShowHelp();
                }
            });
        });

        app.Command("midrange", midRangeCommand =>
        {
            var rounding = midRangeCommand.Option("-r|--round", "", CommandOptionType.SingleValue);

        });

        app.Command("interquartilemean", iqrMean =>
        {

        });


        app.OnExecute(() =>
        {
            if(version.HasValue()) 
            {
                ConsoleHelper.PrintVersion();
            }

            if(help.HasValue())
            {
                app.ShowHelp();
            }
            if (license.HasValue())
            {
                ConsoleHelper.PrintUnformattedLicense();
            }
        });

        return app.Execute(args);
    }
}
