import 'dart:io';

import 'package:args/args.dart';

import 'formatter.dart';
import 'rounding.dart';

const String version = '0.0.1';

ArgParser buildParser() {
  return ArgParser()
    ..addOption(
      'mode',
      abbr: 'm',
      mandatory: false,
      valueHelp: 'Either "significant-figures"/"sf" or "decimal-places"/"dp" are supported.'
    )
    ..addOption(
      'precision',
      abbr: '-p',
      mandatory: false,
      defaultsTo: 2.toString(),
      help: 'The number of either Decimal Places or Significant Figures to use depending on the chosen mode. If no mode is specified decimal places are used.'
    )
    ..addOption(
      'file',
      abbr: 'f',
      help: 'Save the results to a specified file.',
      mandatory: false,
    )
    ..addFlag(
        'output',
        abbr: 'o',
        negatable: true,
        help: 'Prints the results to the command line.',
        defaultsTo: true
    )
    ..addFlag(
      'help',
      abbr: 'h',
      negatable: false,
      help: 'Print this usage information.',
    )
    ..addFlag(
      'verbose',
      abbr: 'vb',
      negatable: false,
      help: 'Show additional command output.',
    )
    ..addFlag(
      'version',
      abbr: 'v',
      negatable: false,
      help: 'Print the tool version.',
    );
}

void saveToFile(String filePath, List<String> numbers){
  File file = File(filePath);

  file.writeAsString(numbers.toString());
}

void writelnList(List<double> doubles){
  for(double d in doubles){
    writeln(d);
  }
}

void writeln(double number){
  stdout.writeln(Formatter.formatNum(number));
}

void printUsage(ArgParser argParser) {
  print('Usage: round <options> <flags> [arguments]');
  print(argParser.usage);
}

void main(List<String> arguments) {
  final ArgParser argParser = buildParser();
  try {
    final ArgResults results = argParser.parse(arguments);
    bool verbose = false;

    /**
     * Format the rest args into doubles.
     */
    List<double> numbers = List.empty();
    for(int index = 0; index < results.rest.length; index++) {
      numbers.add(double.parse(results.rest[index]));
    }

    var precision = int.parse(results['precision']);

    List<String> newNumbers = List.empty();

    if(results.wasParsed('mode')){
      var mode = results['mode'].toString().toLowerCase();
      if(mode.contains("significant-figures") || mode.contains("sigfig") || mode.contains("sf")){
        newNumbers = Rounding.roundListToSignificantFigures(numbers, precision);
      }
      else if(mode.contains("decimal-places") || mode.contains("decimal") || mode.contains("dp")){
        newNumbers = Rounding.roundListToDecimalPlaces(numbers, precision);
      }
    }

   List<String> strings = Formatter.formatStringsAsDecimals(newNumbers);

    if(results.wasParsed("file")){
      saveToFile(results['file'], strings);
    }

    if(results.wasParsed('output')){
      if(results['output'] == true){
        writelnList(numbers);
      }
      return;
    }

    // Process the parsed arguments.
    if (results.wasParsed('help')) {
      printUsage(argParser);
      return;
    }
    if (results.wasParsed('version')) {
      print('round version: $version');
      return;
    }
    if (results.wasParsed('verbose')) {
      verbose = true;
    }

    // Act on the arguments provided.
    print('Positional arguments: ${results.rest}');
    if (verbose) {
      print('[VERBOSE] All arguments: ${results.arguments}');
    }
  } on FormatException catch (e) {
    // Print usage information if an invalid argument was provided.
    print(e.message);
    print('');
    printUsage(argParser);
  }
}
