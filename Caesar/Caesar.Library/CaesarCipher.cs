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

using System.Text;

namespace Caesar.Library;

public class CaesarCipher
{
    protected string Alphabet = "abcdefghijklmnopqrstuvwxyz";

    internal int GetAlphabetPosition(char c)
    {
        for (int index = 0; index < Alphabet.Length; index++)
        {
            if (Alphabet[index].ToString().Equals(c.ToString().ToLower()))
            {
                return index;
            }
        }

        throw new ArgumentException("The value provided was not in the alphabet.");
    }

    internal int OldPositionCalculator(int newPosition, int shift)
    {
        if (newPosition - shift > 0 && newPosition - shift < 26)
        {
            return newPosition - shift;
        }
        else
        {
            int xMoreThan25 = 0;
            
            if (shift > 25)
            {
                xMoreThan25 = shift % 25;
            }
            shift -= (xMoreThan25 * shift);
            
            return newPosition - shift;
        }
    }
    
    internal int NewPositionCalculator(int oldPosition, int shift)
    {
        if (oldPosition + shift < 26)
        {
            return oldPosition + shift;
        }
        else
        {
            int xMoreThan25 = 0;
            
            if (shift > 25)
            {
                xMoreThan25 = shift % 25;
            }

            if (xMoreThan25 > 1)
            {
                shift -= (xMoreThan25 * shift);
            }
            
            return oldPosition + shift;
        }
    }
    
    public string Encode(string value, int shift)
    {
        StringBuilder stringBuilder = new StringBuilder();
        
        for (int characterIndex = 0; characterIndex < value.Length; characterIndex++)
        {
            char character = value[characterIndex];

            if (character.ToString().Equals(character.ToString().ToUpper()))
            {
                character = Convert.ToChar(Alphabet[NewPositionCalculator(GetAlphabetPosition(character), shift)].ToString().ToUpper());
            }
            else if (character.ToString().Equals(character.ToString().ToLower()))
            {
                character = Alphabet[NewPositionCalculator(GetAlphabetPosition(character), shift)];
            }

            stringBuilder.Append(character);
        }

        return stringBuilder.ToString();
    }
    
    public string[] Encode(string[] values, int shift)
    {
        List<string> strings = new List<string>();

        foreach (string value in values)
        {
            strings.Add(Encode(value, shift));
        }
        return strings.ToArray();
    }

    public string Decode(string value, int shift)
    {
        StringBuilder stringBuilder = new StringBuilder();
        
        for (int characterIndex = 0; characterIndex < value.Length; characterIndex++)
        {
            char character = value[characterIndex];

            if (character.ToString().Equals(character.ToString().ToUpper()))
            {
                character = Convert.ToChar(Alphabet[OldPositionCalculator(GetAlphabetPosition(character), shift)].ToString().ToUpper());
            }
            else if (character.ToString().Equals(character.ToString().ToLower()))
            {
                character = Alphabet[OldPositionCalculator(GetAlphabetPosition(character), shift)];
            }

            stringBuilder.Append(character);
        }

        return stringBuilder.ToString();
    }

    public string[] Decode(string[] values, int shift)
    {
        List<string> strings = new List<string>();

        foreach (string value in values)
        {
            strings.Add(Decode(value, shift));
        }
        return strings.ToArray();
    }
}