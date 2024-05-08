import 'dart:math';

class Power{

  static double power(double number, double exponent){
    return pow(number, exponent) as double;
  }

  static List<double> powerList(List<double> numbers, double exponent){
    List<double> doubles = List.empty();

    for(int index = 0; index < numbers.length; index++){
      doubles.add(power(numbers[index], exponent));
    }

    return doubles;
  }
}