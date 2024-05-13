using Spectre.Console.Cli;

namespace Average;

public class BaseSettings : CommandSettings
{
    [CommandArgument(0, "<numbers>")]
    public decimal[]? Inputs { get; init; }
        
    [CommandOption("-o|--output:[file]")]
    public string? FileOutput { get; init; }
}