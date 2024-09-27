using Today.Library.Abstractions;

namespace Today.Library;

public class DateExtender : IDateExtender
{
    public int GetWeekOfYear(DateTime date)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public int GetDayOfMonth(DateTime date)
    {
        return date.Day;
    }
}