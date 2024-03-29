package com.alastairlundy.utils.pow.commands;

import com.github.rvesse.airline.annotations.Arguments;
import com.github.rvesse.airline.annotations.Command;
import com.github.rvesse.airline.annotations.Option;

@Command(name = "root", description = "Calculate the root of any number")
public class Root {

    @Option(name = {"-p", "--power"}, description = "Calculates the root of a value to a given power.")
    private boolean usePower = false;

    @Arguments
    private double[] arguments;

    public static double squareRoot(double value){
        return Math.sqrt(value);
    }

    public static double cubeRoot(double value){
        return Math.cbrt(value);
    }

    public static double root(double value){
        return Math.pow(value, 1.0 / value);
    }

    public run(){

    }
}
