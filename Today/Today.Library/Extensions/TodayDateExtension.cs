namespace Today.Library.Extensions;

public static class TodayDateExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static string LongToday(this DateTime dateTime)
    {
        return dateTime.ToString("R").Replace(",", string.Empty);
    }
}