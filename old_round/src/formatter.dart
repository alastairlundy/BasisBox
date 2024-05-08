import 'package:decimal/decimal.dart';

class Formatter{
  static Decimal formatNum(num number){
    return Decimal.parse(number.toString());
  }

  static Decimal formatString(String string){
    return Decimal.parse(double.parse(string).toString());
  }

  static List<String> formatStringsAsDecimals(List<String> numbers){
    List<String> strings = List.empty();

    for(int index = 0; index < numbers.length; index++){
      strings.add(formatString(numbers[index]).toString());
    }

    return strings;
  }

  static List<String> formatDecimals(List<num> numbers){
    List<String> strings = List.empty();

    for(int index = 0; index < numbers.length; index++){
      strings.add(formatNum(numbers[index]).toString());
    }

    return strings;
  }

  static String concatenateStrings(List<String> strings){
    String string = "";

    for(String str in strings){
      string += str;
    }

    return string;
  }
}