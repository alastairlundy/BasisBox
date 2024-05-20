﻿using System.ComponentModel;
using Spectre.Console.Cli;

namespace Parrot.Cli.Commands;

public class ParrotCommand : Command<ParrotCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<Lines To Print>")]
        public string[]? LinesToPrint { get; init; }
            
        [CommandOption("-n")]
        [DefaultValue(false)]
        public bool DisableTrailingNewLine { get; init; }
        
        [CommandOption("-e")]
        [DefaultValue(false)]
        public bool EnableParsingOfBackslashEscapeChars { get; init; }
        
        [CommandOption("-E")]
        [DefaultValue(true)]
        public bool DisableInterpretationOfBackslashEscapeChars { get; init; }
    }


    public override int Execute(CommandContext context, Settings settings)
    {
        if (settings.LinesToPrint == null)
        {
            if(context)
        }
        
        if (settings.DisableInterpretationOfBackslashEscapeChars &&
            settings.EnableParsingOfBackslashEscapeChars == false)
        {
             
        }
        
        
        
        throw new NotImplementedException();
    }
}