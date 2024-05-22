
using System;
using System.Reflection;

using AlastairLundy.Extensions.System.AssemblyExtensions;
using AlastairLundy.Extensions.System.VersionExtensions;

using Parrot.Cli.Commands;

using Spectre.Console.Cli;

CommandApp app = new();

app.Configure(config =>
{
    config.UseAssemblyInformationalVersion();
    
    config.AddCommand<EchoCommand>("")
    .WithAlias("echo");

    config.SetApplicationVersion(Assembly.GetExecutingAssembly().GetProjectVersion().GetFriendlyVersionToString());
});

app.SetDefaultCommand<EchoCommand>();

return app.Run(args);