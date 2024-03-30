Calculates square root, cube root, and any value to a positive or negative power.

## Usage

### Flags

| Flag Name | Abbreviation | Description                                                                  | Default Value (if any) | Is Negatable | 
|-----------|--------------|------------------------------------------------------------------------------|------------------------|--------------|
| output    | o            | Whether to print the results of the calculations to the Standard Output.     | true                   | Yes          |
| help      | h            | Print the usage information and arguments and flags that can be used.        | false                  | No           |
| version   | v            | Prints the program's version to the Standard Output.                         | false                  | No           |
| verbose   | ve           | Expands upon the command output, particularly when also using the help flag. | false                  | No           |


### Options
| Flag Name | Abbreviation | Description                                                          | Mandatory | Possible values                                |
|-----------|--------------|----------------------------------------------------------------------|-----------|------------------------------------------------|
| mode      | m            | Sets the mode to use for calculating the power or root of the values | false     | squareroot/sqrt, cuberoot/cbrt, power/pwr/root |
| file      | f            | Save the results to a specified file.                                | false     | [file path]/[file name]                        |
| exponent  | e            | The exponent to use if using the Power command.                      | false     |                                                |


### Arguments
The list of numbers to apply calculations to. If no exponent is provided when using power/pwr/root, the last number provided will be used as the exponent.