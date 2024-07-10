
using Moment.Cli.Commands;

using Spectre.Console.Cli;

CommandApp app = new CommandApp();

app.Configure(config =>
{

    config.AddCommand<MomentCommand>("");

});

app.SetDefaultCommand<MomentCommand>();

return app.Run(args);