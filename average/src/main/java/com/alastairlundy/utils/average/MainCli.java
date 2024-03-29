package com.alastairlundy.utils.average;

import com.github.rvesse.airline.annotations.Cli;

import com.alastairlundy.utils.average.commands.Mean;
import com.alastairlundy.utils.average.commands.Median;
import com.alastairlundy.utils.average.commands.Mode;
import com.alastairlundy.utils.average.commands.Root;

@Cli(name = "average",
        description = "Calculate averages easily",
        defaultCommand = Mean.class,
        commands = {Mean.class, Root.class, Mode.class, Median.class}
)
public class MainCli {


}
