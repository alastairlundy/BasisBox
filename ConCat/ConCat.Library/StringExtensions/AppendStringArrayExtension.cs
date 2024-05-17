using System.Collections.Generic;

namespace ConCat.Library.StringExtensions;

public static class AppendStringArrayExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="array"></param>
    /// <param name="arrayToBeAppended"></param>
    /// <returns></returns>
    public static string[] Append(this string[] array, string[] arrayToBeAppended)
    {
        List<string> list = [.. array, .. arrayToBeAppended];

        return list.ToArray();
    }
}