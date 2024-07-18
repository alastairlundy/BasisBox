
using Spectre.Console.Cli;

CommandApp app = new CommandApp();

app.Configure(config =>
{

    
    config.UseAssemblyInformationalVersion();
});

return app.Run(args);