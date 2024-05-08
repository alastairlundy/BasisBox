import 'dart:ffi';

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
    List<String> strings = List.empty(growable: true);

    for(int index = 0; index < numbers.length; index++){
      strings.add(roundToDecimalPlaces(numbers[index], decimalPlaces));
    }

    return strings;
  }
  
  static String roundToDecimalPlaces(double number, int decimalPlaces){
    return finalRoundingChecks(Formatter.formatNum(number).toStringAsFixed(decimalPlaces), decimalPlaces);
  }

  static List<String> roundListToSignificantFigures(List<double> numbers, int decimalPlaces){
    List<String> strings = List.empty(growable: true);

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
     List<String> numbers = number.toString().split(".");
     
     if(numbers.length > 1){
     List<String> decimals = List.empty(growable: true);

       for(int index = numbers[1].length; index > (numbers[1].length - 1); index--){
         int currentSf = SignificantFigures.countTotalSignificantFigures(number.toString());

         String toBeAdded = "";

         if(currentSf > significantFigures){
           if(index > numbers[1].length - 1){
              if(numbers[1][index + 1] != 0.toString()){

                if(double.parse(numbers[1][index + 1]) >= 5){
                  toBeAdded = (double.parse(numbers[1][index]) + 1).toString();
                }
                else{
                  toBeAdded = 0.toString();
                }

               decimals[index + 1] = 0.toString();
              }
           }
         }

         decimals.insert(0, toBeAdded);
       }

       numbers[1] = numbers[1].replaceRange(numbers[1].indexOf("."), numbers[1].length - 1, Formatter.concatenateStrings(decimals));

       numbers.insert(1, ".");
     }

     List<String> chars = List.empty(growable: true);
     for(int index = numbers[0].length; index > (numbers[0].length - 1); index--){
       int currentSf = SignificantFigures.countTotalSignificantFigures(Formatter.concatenateStrings(numbers));

       String toBeAdded = "";

       if(currentSf > significantFigures){
         if(index > numbers[0].length -1){
           if(numbers[0][index + 1] != 0.toString()){

             if(double.parse(numbers[1][index + 1]) >= 5){
               toBeAdded = (double.parse(numbers[1][index]) + 1).toString();
             }
             else{
               toBeAdded = 0.toString();
             }

             chars[index + 1] = 0.toString();
           }
         }
         chars.insert(0, toBeAdded);
       }
     }

     numbers[0] = numbers[0].replaceRange(0, numbers[0].length, Formatter.concatenateStrings(chars));
     
     var dp = 0;

     if(numbers.length > 1){
       dp = DecimalPlaces.countTotalDecimalPlaces(Formatter.concatenateStrings(numbers));
     }

     return finalRoundingChecks(Formatter.concatenateStrings(numbers), dp);
   }
  }
}