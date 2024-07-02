

using System.Reflection;
using NewDir.Cli.Commands;
using NewDir.Cli.Localizations;

using Spectre.Console.Cli;

CommandApp app = new();

app.Configure(config =>
{
    config.AddCommand<NewDirCommand>("")
        .WithDescription(Resources.Commands_NewDir_Description);

    config.SetApplicationName(Assembly.GetExecutingAssembly().GetName().Name!);
    config.UseAssemblyInformationalVersion();
});

app.SetDefaultCommand<NewDirCommand>();

return app.Run(args);