using AlastairLundy.Extensions.System.VersionExtensions;
using AlastairLundy.Extensions.System.VersionExtensions.Enums;

using McMaster.Extensions.CommandLineUtils;
using System.Reflection;

namespace average
{
    internal class Program
    {
        static int Main(string[] args)
        {
            CommandLineApplication app = new CommandLineApplication();

            Version appVersion = Assembly.GetEntryAssembly()!.GetName().Version!;

            var help = app.HelpOption("-h|--help");
            var version = app.VersionOption("-v|--version", appVersion.GetFriendlyVersionToString(FriendlyVersionFormatStyle.MajorDotMinorDotBuild), appVersion.ToString());

            app.Command("mean", meanCommand =>
            {
                var geometricFlag = meanCommand.Option("--geometric", "Calculate the geometric mean", CommandOptionType.SingleOrNoValue);
                var arithmeticFlag = meanCommand.Option("-arithmetic", "Calculate the arithmetic mean.", CommandOptionType.SingleOrNoValue).DefaultValue = "true";

                var rounding = meanCommand.Option("-dp | --decimal-places", "", CommandOptionType.SingleValue).DefaultValue = "2";

                var numbers = meanCommand.Argument("<numbers>", "The numbers to get the average of.", true);




            });

            app.Command("median", medianCommand =>
            {

            });

            app.Command("mode", modeCommand =>
            {

            });

            app.Command("midrange", midRangeCommand =>
            {

            });

            app.Command("interquartilemean", iqrMean =>
            {

            });


            app.OnExecute(() =>
            {
                if(version.HasValue()) 
                {
                    
                }

                if(help.HasValue())
                {
                    app.ShowHelp();
                }
            });

            return app.Execute(args);
        }
    }
}
