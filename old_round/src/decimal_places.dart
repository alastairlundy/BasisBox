class DecimalPlaces{

  static int countTotalDecimalPlaces(String number){
    int numberOfDecimalPlaces = 0;

    bool seenDecimal = false;

    if(number.contains(".")){
      for(int index = 0; index < number.length; index++){
        String c = number[index];

        if(c == '.'){
          seenDecimal = true;
        }

        if(seenDecimal){
          if(double.parse(c).isFinite){
            numberOfDecimalPlaces++;
          }
        }
      }

      return numberOfDecimalPlaces;
    }
    else{
      return numberOfDecimalPlaces;
    }
  }
}