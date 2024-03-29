package com.alastairlundy.utils.pow;


import com.alastairlundy.utils.pow.commands.Power;
import com.alastairlundy.utils.pow.commands.Root;
import com.github.rvesse.airline.annotations.Cli;

@Cli(name = "average",
        description = "Calculate averages easily",
        defaultCommand = Root.class,
        commands = {Root.class, Power.class}
)
public class MainCli {


}
