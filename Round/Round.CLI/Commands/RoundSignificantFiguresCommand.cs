/*

   Copyright 2024 Alastair Lundy

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
 
 */

using System;

using Round.Cli.Settings;

using Spectre.Console.Cli;

namespace Round.Cli.Commands;

internal class RoundSignificantFiguresCommand : Command<RoundSignificantFiguresCommand.Settings>
{
    public override int Execute(CommandContext context, Settings settings)
    {
        throw new NotImplementedException();
    }

    internal class Settings: BaseSettings
    {
        [CommandArgument(0, "<number_to_round>")]
        public decimal? NumberToRound { get; init; }

        [CommandArgument(1, "<number_of_significant_figures>")]
        public int? NumberOfSignificantFiguresToRoundTo { get; init; }
    }
}
