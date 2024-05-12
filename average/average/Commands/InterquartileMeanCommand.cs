using Spectre.Console.Cli;

namespace average.Commands;

public class InterquartileMeanCommand : Command<InterquartileMeanCommand.Settings>
{
    public class Settings : CommandSettings
    {
        
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        throw new NotImplementedException();
    }
}