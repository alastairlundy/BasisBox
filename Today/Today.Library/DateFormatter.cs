using Today.Library.Abstractions;

namespace Today.Library;

public class DateFormatter : IDateFormatter
{
    /// <summary>
    /// `
    /// </summary>
    /// <param name="dateString"></param>
    /// <returns></returns>
    public string GivenDateToString(string dateString)
    {
        return GivenDateToString(DateTime.Parse(dateString));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dateString"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public bool TryParseGivenDate(string dateString, out string? result)
    {
        try
        {
            result = GivenDateToString(dateString);
            return true;
        }
        catch
        {
            result = null;
            return false;
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public string GivenDateToString(DateTime date)
    {
        return date.ToString("R");
    }
    
    /// <summary>
    /// Returns the day of the week as a number from 1 (Monday) to 7 (Sunday).
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public int DayOfWeek(DateTime date)
    {
        int dayOfWeek;
        if (date.DayOfWeek == System.DayOfWeek.Sunday)
        {
            dayOfWeek = 7;
        }
        else
        {
            dayOfWeek = (int)date.DayOfWeek;
        }


        return dayOfWeek;
    }

    public string ShortMonthName(DateTime date)
    {
        return date.ToString("MMM");
    }

    public string LongMonthName(DateTime date)
    {
        return date.ToString("MMMM");
    }

    public string FullWeekDayName(DateTime date)
    {
        return date.ToString("dddd");
    }

    public string ShortWeekDayName(DateTime date)
    {
        return date.ToString("ddd");
    }
}