package com.alastairlundy.utils.average.commands;

import com.github.rvesse.airline.annotations.Arguments;
import com.github.rvesse.airline.annotations.Command;
import com.github.rvesse.airline.annotations.Option;

@Command(name = "mean", description = "Calculate averages using either arithmetic or geometric mean.")
public class Mean {

    @Arguments
    private double[] inputs;

    @Option(name = {"-g", "--geometric"}, description = "Calculate a Geometric Mean rather than an Arithmetic one.")
    private boolean useGeometricMean = false;

    @Option(name = {"-r", "--round"}, description = "The number of decimals the result should be rounded.")
    private int roundingDigits = 0;

    public int runGeometric(){
        try{
            double sum = 0;

            for (double input : inputs) {
                sum *= input;
            }

            sum = Root.root(sum,inputs.length);

            System.out.println(sum);

        }
        catch (Exception exception){
            return
        }
    }

    private int runArithmetic(){
       try{
           double sum = 0;

           for (double input : inputs) {
               sum += input;
           }

           sum = sum / inputs.length;

           System.out.println(sum);

       }
       catch (Exception exception){
           return
       }
    }

    public void run(){

    }

}
