import 'package:decimal/decimal.dart';

class Formatter{
  static Decimal formatNum(num number){
    return Decimal.parse(number.toString());
  }

  static List<String> formatDecimals(List<num> numbers){
    List<String> strings = List.empty();

    for(int index = 0; index < numbers.length; index++){
      strings.add(formatNum(numbers[index]).toString());
    }

    return strings;
  }
}