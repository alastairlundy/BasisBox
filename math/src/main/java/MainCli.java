import com.github.rvesse.airline.annotations.Cli;

import commands.Mean;

@Cli(name = "mean",
        description = "Calculate averages easily",
        defaultCommand = Mean.class,
        commands = {Mean.class}
)
public class MainCli {


}
