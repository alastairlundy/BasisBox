using System;
using System.ComponentModel;

using Spectre.Console.Cli;

namespace Round.Cli.Settings;

internal class BaseSettings : CommandSettings
{
    [CommandOption("-v|--version")]
    public string? Version { get; init; }

    [CommandOption("-h|--help")]
    public string? Help { get; init; }

    [CommandOption("-p|--pretty")]
    [DefaultValue(false)]
    public bool PrettyMode { get; init; }
}
