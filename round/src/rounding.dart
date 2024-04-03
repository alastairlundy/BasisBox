import 'package:decimal/decimal.dart';

import 'decimal_places.dart';
import 'formatter.dart';
import 'significant_figures.dart';

class Rounding{

  static String finalRoundingChecks(String number, int decimalPlaces){
    if(number.endsWith("5")){
     double val = double.parse(number[number.length - 1]);
     val += 1;

     number.replaceRange(number.length - 1, number.length - 1, val.toString());

      if(decimalPlaces > 0){
        return double.parse(number).toStringAsFixed(decimalPlaces - 1);
      }
      else{
       return double.parse(number).toStringAsFixed(decimalPlaces);
      }
    }
    else{
      return number;
    }
  }

  static List<String> roundListToDecimalPlaces(List<double> numbers, int decimalPlaces){
    List<String> strings = List.empty();

    for(int index = 0; index < numbers.length; index++){
      strings.add(roundToDecimalPlaces(numbers[index], decimalPlaces));
    }

    return strings;
  }
  
  static String roundToDecimalPlaces(double number, int decimalPlaces){
    return finalRoundingChecks(Formatter.formatNum(number).toStringAsFixed(decimalPlaces));
  }

  static List<String> roundListToSignificantFigures(List<double> numbers, int decimalPlaces){
    List<String> strings = List.empty();

    for(int index = 0; index < numbers.length; index++){
      strings.add(roundToSignificantFigures(numbers[index], decimalPlaces));
    }

    return strings;
  }

  static String roundToSignificantFigures(double number, int significantFigures){
   int existingSignificantFigures = SignificantFigures.countTotalSignificantFigures(number.toString());
 
   if(existingSignificantFigures == significantFigures || existingSignificantFigures < significantFigures){
     return finalRoundingChecks(number.toString(), DecimalPlaces.countTotalDecimalPlaces(number.toString()));
   }
   else{
     var string = number.toString();

     var numbers = string.split(".");


   }
  }
}