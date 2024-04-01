class SignificantFigures{

  

  static int countTotalSignificantFigures(String number){
    int numberOfSignificantFigures = 0;

    String val = number.toString();

    List<String> split = val.split(".");

    if(split.length > 1){
      for(int index = 0; index < split[1].length; index++){
        numberOfSignificantFigures++;
      }
    }

    for(int index = 0; index < split[0].length; index++){
      int i = int.parse(split[0][index]);

      if(i != 0){
        numberOfSignificantFigures++;
      }
    }

    return numberOfSignificantFigures;
  }
}