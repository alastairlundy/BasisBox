using Spectre.Console.Cli;

namespace average.Commands;

public class ModeCommand : Command<ModeCommand.Settings>
{
    public class Settings : CommandSettings
    {
        
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        throw new NotImplementedException();
    }
}