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

using System.Globalization;

using Spectre.Console;

namespace Average.Helpers;

public static class GridCreator
{
    public static Grid CreateGridWithRows(decimal[] values)
    {
        Grid grid = new Grid();

        grid.AddColumn();

        foreach (decimal value in values)
        {
            grid.AddRow(new Text(value.ToString(CultureInfo.CurrentCulture)));
        }

        return grid;
    }
}