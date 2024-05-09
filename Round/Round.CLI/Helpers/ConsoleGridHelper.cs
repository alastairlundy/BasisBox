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

using Spectre.Console;


namespace Round.Cli.Helpers;

internal class ConsoleGridHelper
{

    internal static Grid CreateResultGrid(Text providedValueLabel, Text providedValueActual, Text providedPrecisionLabel, Text providedPrecisionActual, Text resultLabel, Text resultActual)
    {
        Grid grid = new();

        grid.AddRow(
    new Padder[]
    {
                    new Padder(providedValueLabel).PadBottom(10),
                    new Padder(providedValueActual).PadBottom(10)
    });

        grid.AddRow(
            new Padder[] {
                    new Padder(providedPrecisionLabel).PadBottom(10),
                    new Padder(providedPrecisionActual).PadBottom(10)
        });
        grid.AddRow(
            new Padder[]
            {
                    new Padder(resultLabel).PadBottom(10),
                     new Padder(resultActual).PadBottom(10)
            });

        return grid;
    }
}
