using System.ComponentModel;
using Spectre.Console.Cli;

namespace ConCat.Cli.Settings;

public class BasicConCatSettings : CommandSettings
{
    [CommandArgument(0, "<File(s)>")]
    public string[]? Files { get; init; }

    [CommandOption("-n")]
    [DefaultValue(false)]
    public bool AppendLineNumbers { get; init; }
        
    [CommandOption("--verbose|--debug")]
    [DefaultValue(false)]
    public bool ShowErrors { get; init; }
}