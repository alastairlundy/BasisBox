using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Moment.Cli.Localizations;
using PlatformKit;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Moment.Cli.Commands;

public class MomentCommand : Command<MomentCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<command>")]
        [DefaultValue(null)]
        public string? Command { get; init; }
        
        [CommandOption("--debug|--debugging|--show-errros")]
        [DefaultValue(false)]
        public bool ShowErrors { get; init; }
        
        [CommandOption("-p")]
        [DefaultValue(null)]
        public string? Mode { get; init; }
        
        [CommandOption("--hide-command")]
        [DefaultValue(false)]
        public bool HideCommandWindow { get; init; }
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        Stopwatch userTimeStopWatch = new Stopwatch();
        Stopwatch systemTimeStopWatch = new Stopwatch();
        
        systemTimeStopWatch.Start();

        if (settings.Command == null)
        {
            AnsiConsole.WriteException(new ArgumentNullException(nameof(settings.Command), Resources.Exceptions_InvalidArgument));
            return -1;
        }

        string result;
        
        try
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.UseShellExecute = true;

            processStartInfo.UserName = Environment.UserName;
            processStartInfo.WorkingDirectory = Environment.CurrentDirectory;
            
            string[] args = settings.Command.Split(' ');
            
            arg
            args.Except(args, args[0]);
            
            if (settings.HideCommandWindow)
            {
                processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            }
            else
            {
                processStartInfo.WindowStyle = ProcessWindowStyle.Normal;
            }
            
            
            if (OperatingSystem.IsWindows())
            {

            }
            if (OperatingSystem.IsLinux() || OperatingSystem.IsFreeBSD())
            {
                result = 
            }
            if (OperatingSystem.IsMacOS())
            {
                result = 
            }


            return 0;
        }
        catch(Exception exception)
        {
            AnsiConsole.WriteException(exception);
            return -1;
        }
    }
}