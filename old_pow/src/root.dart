import 'dart:math';

import 'package:decimal/decimal.dart';

class Root{

  static double squareRootOf(double number){
    return sqrt(number);
  }

  static List<double> squareRootOfToList(List<double> numbers){
    List<double> numberList = List.empty();

    for(int index = 0; index < numbers.length; index++){
      numberList.add(squareRootOf(numbers[index]));
    }

    return numberList;
  }

  static num cubeRootOf(double number){
    return pow(number, 1/3);
  }

  static List<double> cubeRootOfToList(List<double> numbers){
    List<double> numberList = List.empty();

    for(int index = 0; index < numbers.length; index++){
      numberList.add(cubeRootOf(numbers[index]) as double);
    }

    return numberList;
  }
}