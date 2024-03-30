import 'package:args/args.dart';
import 'dart:io';

import 'formatter.dart';
import 'power.dart';
import 'root.dart';

const String version = '0.1.0';

ArgParser buildParser() {
  return ArgParser()
    ..addOption(
      'mode',
      abbr: 'm',
      help: "Sets the mode to use for calculating the power or root of the values",
      mandatory: false
    )
    ..addOption(
      'file',
      abbr: 'f',
      help: 'Save the results to a specified file.',
      mandatory: false,
    )
    ..addOption(
      'exponent',
      abbr: 'e',
      help: 'The exponent to use',
      mandatory: false
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

void printUsage(ArgParser argParser) {
  print('Usage: dart cli.dart <flags> [arguments]');
  print(argParser.usage);
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

    if(results.wasParsed("mode")){
        var mode = results['mode'].toString();

        if(mode.contains("sqrt") || mode.contains("squareroot")){
          numbers = Root.squareRootOfToList(numbers);
        }
        if(mode.contains("cbrt") || mode.contains("cuberoot")){
          numbers = Root.cubeRootOfToList(numbers);
        }
        if(mode.contains("power") || mode.contains("root")){
          double exponent;

          if(results.wasParsed('exponent')){
            exponent = results['exponent'];
          }
          else{

            if(results.rest.length > 1){
              exponent = double.parse(results.rest[results.rest.length - 1]);
            }
            else{
              exponent = double.parse(results.rest[0]);
            }

          }

          numbers = Power.powerList(numbers, exponent);
        }
    }

    List<String> strings = Formatter.formatDecimals(numbers);

    if(results.wasParsed("file")){
      saveToFile(results['file'], strings);
    }

    if(results.wasParsed('output')){
      if(results['output'] == true){
        writelnList(numbers);
      }
    }

    // Process the parsed arguments.
    if (results.wasParsed('help')) {
      printUsage(argParser);
      return;
    }
    if (results.wasParsed('version')) {
      print('pow version: $version');
      return;
    }
    if (results.wasParsed('verbose')) {
      verbose = true;
    }

    // // Act on the arguments provided.
    // print('Positional arguments: ${results.rest}');
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
