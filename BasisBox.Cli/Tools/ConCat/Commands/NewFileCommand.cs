/*

    BasisBox - ConCat
    Copyright (C) 2024 Alastair Lundy

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Lesser General Public License as published by
    the Free Software Foundation, version 3 of the License.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Lesser General Public License for more details.

    You should have received a copy of the GNU Lesser General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using BasisBox.Cli.Localizations;
using BasisBox.Cli.Tools.ConCat.Helpers;
using BasisBox.Cli.Tools.ConCat.Settings;
using Spectre.Console;
using Spectre.Console.Cli;

namespace BasisBox.Cli.Tools.ConCat.Commands;

public class NewFileCommand : Command<NewFileCommand.Settings>
{
    public class Settings : BasicConCatSettings
    {
        
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        if (settings.Files == null || settings.Files.Any() == false)
        {
            AnsiConsole.WriteException(new NullReferenceException(Resources.Exceptions_NoFileProvided));
            return -1;
        }
        
        try
        {
            string fileName = "";
            if (settings.Files.Any() && settings.Files.First().StartsWith(">") == false)
            {
                fileName = AnsiConsole.Prompt(new TextPrompt<string>(Resources.ConCat_App_Commands_NewFile_Prompt));
            }
            else if (settings.Files.Any() && settings.Files.First().StartsWith(">"))
            {
                fileName = settings.Files.First().Replace(">", string.Empty);
            }

            StringBuilder stringBuilder = new StringBuilder();

            ConsoleKeyInfo keyInfo;
                
            do
            {
                while (Console.KeyAvailable == false)
                {
                    Thread.Sleep(250);
                }
                    
                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key != ConsoleKey.D && (keyInfo.Modifiers != ConsoleModifiers.Control))
                {
                    if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        stringBuilder.AppendLine();
                    }
                    else
                    {
                        stringBuilder.Append(keyInfo.KeyChar);
                    }
                }

            } while (keyInfo.Key != ConsoleKey.D && (keyInfo.Modifiers != ConsoleModifiers.Control));
            
            string[] fileContents = stringBuilder.ToString().Split(Environment.NewLine);
            
            File.WriteAllLines(fileName, fileContents);
                
            AnsiConsole.WriteLine(Resources.ConCat_App_Commands_NewFile_Success.Replace("{x}", settings.Files.First()));

            return 1;
        }
        catch (UnauthorizedAccessException exception)
        {
            return ConsoleHelper.HandleException(exception,
                Resources.Exceptions_Permissions_Invalid.Replace("{x}", exception.Source), settings.ShowErrors);
        }
        catch (FileNotFoundException exception)
        {
            return ConsoleHelper.HandleException(exception,
                Resources.Exceptions_FileNotFound.Replace("{x}", exception.Source), settings.ShowErrors);
        }
        catch (Exception exception)
        {
            return ConsoleHelper.HandleException(exception,
                Resources.Exceptions_Generic.Replace("{x}", exception.Source), settings.ShowErrors);
        }
    }
}