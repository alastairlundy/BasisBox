package com.alastairlundy.utils.pow;

import com.alastairlundy.utils.average.commands.Mean;
import com.alastairlundy.utils.average.commands.Median;
import com.alastairlundy.utils.average.commands.Mode;
import com.alastairlundy.utils.average.commands.Root;
import com.github.rvesse.airline.annotations.Cli;

@Cli(name = "average",
        description = "Calculate averages easily",
        defaultCommand = Root.class,
        commands = {Mean.class, Root.class, Mode.class, Median.class}
)
public class MainCli {


}
