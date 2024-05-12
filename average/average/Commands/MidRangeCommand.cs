using Spectre.Console.Cli;

namespace average.Commands;

public class MidRangeCommand : Command<MidRangeCommand.Settings>
{
    public class Settings : CommandSettings
    {
        
    }


    public override int Execute(CommandContext context, Settings settings)
    {
        throw new NotImplementedException();
    }
}