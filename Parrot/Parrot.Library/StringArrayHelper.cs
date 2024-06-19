using System;
using System.Text;

namespace Parrot.Library;

public static class StringArrayHelper
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="array"></param>
    /// <returns></returns>
    public static string ToString(this string[] array)
    {
        StringBuilder stringBuilder = new();

        foreach (string str in array)
        {
            stringBuilder.Append(str);
        }

        return stringBuilder.ToString();
    }
}
