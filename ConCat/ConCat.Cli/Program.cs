
using ConCat.Cli.Commands;

using Spectre.Console.Cli;

CommandApp app = new CommandApp();

app.Configure(config =>
{

    config.AddCommand<ConcatenateCommand>("");
});