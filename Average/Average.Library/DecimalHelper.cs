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

namespace Average.Library;

public class DecimalHelper
{
    public static decimal[] ConvertInputToDecimal(string[] values)
    {
        List<decimal> newValues = new List<decimal>();

        foreach (string value in values)
        {
            newValues.Add(decimal.Parse(value));
        }

        return newValues.ToArray();
    }

    public static string[] ConvertDecimalsToStrings(decimal[] values)
    {
        List<string> list = new List<string>();

        foreach (decimal value in values)
        {
            list.Add(value.ToString(CultureInfo.CurrentCulture));
        }

        return list.ToArray();
    }
}