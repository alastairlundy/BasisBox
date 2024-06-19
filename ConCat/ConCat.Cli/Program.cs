
using System.Reflection;

using AlastairLundy.Extensions.System.AssemblyExtensions;
using AlastairLundy.Extensions.System.VersionExtensions;

using ConCat.Cli.Commands;

using Spectre.Console.Cli;

CommandApp app = new();

app.Configure(config =>
{

    config.AddCommand<ConcatenateCommand>("")
        .WithAlias("cat");
    
    config.SetApplicationVersion(Assembly.GetExecutingAssembly().GetProjectVersion().GetFriendlyVersionToString());
});