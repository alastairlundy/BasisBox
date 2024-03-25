package commands;

import com.github.rvesse.airline.annotations.Arguments;
import com.github.rvesse.airline.annotations.Command;
import com.github.rvesse.airline.annotations.Option;

@Command(name = "root", description = "Calculate the root of any number")
public class Root {

    @Arguments
    private double[] value;


    public static double root(double value){
        return Math.pow(value, 1.0 / value);
    }

    public static double root(double value, double power){
        return Math.pow(value, 1.0 / power);
    }

    public run(){

    }
}
