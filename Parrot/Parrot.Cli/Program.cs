
using System.Reflection;

using AlastairLundy.Extensions.System;

using Parrot.Cli.Commands;

using Spectre.Console.Cli;

CommandApp app = new();

app.Configure(config =>
{
    config.UseAssemblyInformationalVersion();
    
    config.AddCommand<EchoCommand>("")
    .WithAlias("echo");

    config.SetApplicationVersion(Assembly.GetExecutingAssembly().GetName().Version.ToFriendlyVersionString());
});

app.SetDefaultCommand<EchoCommand>();

return app.Run(args);