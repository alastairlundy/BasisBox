Round a given number to a specified number of decimal places or significant figures

## Usage

### Flags

| Flag Name | Abbreviation | Description                                                                  | Default Value (if any) | Is Negatable | 
|-----------|--------------|------------------------------------------------------------------------------|------------------------|--------------|
| output    | o            | Whether to print the results of the calculations to the Standard Output.     | true                   | Yes          |
| help      | h            | Print the usage information and arguments and flags that can be used.        | false                  | No           |
| version   | v            | Prints the program's version to the Standard Output.                         | false                  | No           |
| verbose   | vb           | Expands upon the command output, particularly when also using the help flag. | false                  | No           |


### Options
| Flag Name | Abbreviation | Description                                                                                     | Mandatory | Possible values                                           | Default Value (if any)     |
|-----------|--------------|-------------------------------------------------------------------------------------------------|-----------|-----------------------------------------------------------|----------------------------|
| mode      | m            | Sets the mode to use for calculating the power or root of the values                            | false     | significant-figures/sigfig/sf, decimal-places/decimals/dp | Decimal Places/decimals/dp |
| precision | p            | The number of either Decimal Places or Significant Figures to use depending on the chosen mode. | false     |                                                           | 2                          |
| file      | f            | Save the results to a specified file.                                                           | false     | [file path]/[file name]                                   | Not Applicable             |


### Arguments
The list of numbers to apply calculations to. If no exponent is provided when using power/pwr/root, the last number provided will be used as the exponent.