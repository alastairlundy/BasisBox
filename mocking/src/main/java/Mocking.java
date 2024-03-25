import com.github.rvesse.airline.annotations.Arguments;
import com.github.rvesse.airline.annotations.Command;

import java.util.List;

@Command(name = "mocking", description = "Create text in the style of: sPoNgEbOb mOcKiNg tExT.")
public class Mocking {

    @Arguments
    private List<String> words;



    public int run(){

    }

    public static void main(String[] args){

    }

}
