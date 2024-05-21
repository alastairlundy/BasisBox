
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
    
    config.AddCommand<ParrotCommand>("")
    .WithAlias("echo");

    config.SetApplicationVersion(Assembly.GetExecutingAssembly().GetProjectVersion().GetFriendlyVersionToString());
});

app.SetDefaultCommand<ParrotCommand>();

return app.Run(args);