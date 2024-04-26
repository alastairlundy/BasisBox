using System.Reflection;
using System.Security.Cryptography;
using caesar.library;
using caesar.localizations;

using McMaster.Extensions.CommandLineUtils;

namespace caesar;

class Program
{
    static int Main(string[] args)
    {
        CommandLineApplication application = new CommandLineApplication
        {
            Name = Resources.App_Name,
            Description = Resources.App_Description,
        };
        
        var help = application.HelpOption("-h|--help");
        
        var version = application.Option("-v|--version", Resources.Version_Description, CommandOptionType.NoValue);
        
        application.OnExecute(() =>
        {
            if (version.HasValue())
            {
                 AlastairLundy.System.Extensions.Extensions.AssemblyExtensions.AssemblyGetProgramName.GetProjectName(Assembly.GetEntryAssembly());
            }

            if (help.HasValue())
            {
                application.ShowHelp();
            }
        });
        
        application.Command("encode", encodeCommand =>
        {
            var outputOption = encodeCommand.Option($"-o|--output:<{Resources.FilePath_Label}>", Resources.Output_Description, CommandOptionType.SingleValue).
                Accepts(configure => configure.LegalFilePath().NonExistingFile());

            var inputOption = encodeCommand.Option($"-i|--input:<{Resources.FilePath_Label}>", Resources.Input_Description, CommandOptionType.SingleValue)
                .Accepts(configure => configure.LegalFilePath().ExistingFile());

            var shiftOption = encodeCommand.Option($"-s|--shift:<{Resources.Shift_Amount_Description}>", Resources.Shift_Description, CommandOptionType.SingleOrNoValue);

            var words = encodeCommand.Argument("<words>", Resources.Words_Encode_Description, true);
            
            encodeCommand.OnExecute(() =>
            {
                int shift = 0;

                if (shiftOption.HasValue() && shiftOption.Value() != null)
                {
                    shift = int.Parse(shiftOption.Value()!);
                }
                else
                {
                    shift = RandomNumberGenerator.GetInt32(1, 26);
                }

                string[] values;

                if (inputOption.HasValue() &&
                    (inputOption.Value()!.EndsWith(".txt") || inputOption.Value()!.EndsWith(".rtf")))
                {
                   values = ConsoleHelper.ParseInputs(inputOption.Value(), null);
                }
                else
                {
                    if (words.HasValue)
                    {
                       values = ConsoleHelper.ParseInputs(null, words.Value!.Split(" "));
                    }
                    
                    throw new ArgumentException(Resources.Exception_Argument_NoWords);
                }

                CaesarCipher caesarCipher = new CaesarCipher();

                string[] newValues = caesarCipher.Encode(values, shift);

                if (outputOption.HasValue() &&
                    (outputOption.Value()!.EndsWith(".txt") || outputOption.Value()!.EndsWith(".rtf")))
                {
                    File.WriteAllLines(outputOption.Value()!, newValues);
                }
                else
                {
                    ConsoleHelper.PrintResults(values);
                }
            });
        });

        application.Command("decode", decodeCommand =>
        {
            var outputOption = decodeCommand.Option($"-o|--output:<{Resources.FilePath_Label}>", Resources.Output_Description, CommandOptionType.SingleValue).
                Accepts(configure => configure.LegalFilePath().NonExistingFile());

            var inputOption = decodeCommand.Option($"-i|--input:<{Resources.FilePath_Label}>", Resources.Input_Description, CommandOptionType.SingleValue)
                .Accepts(configure => configure.LegalFilePath().ExistingFile());

            var shiftOption = decodeCommand.Option($"-s|--shift:<{Resources.Shift_Amount_Description}>", Resources.Shift_Description, CommandOptionType.SingleOrNoValue);

            var words = decodeCommand.Argument("<words>", Resources.Words_Decode_Description, true);
            
            decodeCommand.OnExecute(() =>
            {
                int shift = 0;

                if (shiftOption.HasValue() && shiftOption.Value() != null)
                {
                    shift = int.Parse(shiftOption.Value()!);
                }
                else
                {
                    shift = RandomNumberGenerator.GetInt32(1, 26);
                }

                string[] values;
            
                if (inputOption.HasValue() &&
                    (inputOption.Value()!.EndsWith(".txt") || inputOption.Value()!.EndsWith(".rtf")))
                {
                   values = ConsoleHelper.ParseInputs(inputOption.Value(), null);
                }
                else
                {
                    if (words.HasValue)
                    {
                       values = ConsoleHelper.ParseInputs(null, words.Value!.Split(" "));
                    }
                    throw new ArgumentException(Resources.Exception_Argument_NoWords);
                }
            
                CaesarCipher caesarCipher = new CaesarCipher();

                string[] newValues = caesarCipher.Decode(values, shift);

                if (outputOption.HasValue() &&
                    (outputOption.Value()!.EndsWith(".txt") || outputOption.Value()!.EndsWith(".rtf")))
                {
                    File.WriteAllLines(outputOption.Value()!, newValues);
                }
                else
                {
                    ConsoleHelper.PrintResults(values);
                }
            });
        });
        
        
       return application.Execute((args));
    }
}