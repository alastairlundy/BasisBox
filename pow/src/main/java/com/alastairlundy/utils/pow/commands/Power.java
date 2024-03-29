package com.alastairlundy.utils.pow.commands;

import com.github.rvesse.airline.annotations.Arguments;
import com.github.rvesse.airline.annotations.Command;
import com.github.rvesse.airline.annotations.Option;

@Command(name = "power", description = "Calculate a value to the power of another value.")
public class Power {

    @Option(name = {"-n", "--negative"}, description = "Calculates a value to a negative power.")
    private boolean calculateNegative;

    @Arguments
    private double[] values;

    @Arguments
    private double power;


    public static double calcToNegativePower(double value, double power){
        return Math.pow(value, 1.0 / power);
    }

    public static double calcToPositivePower(double value, double power){
        return Math.pow(value, power);
    }

    public void run(){

    }
}
