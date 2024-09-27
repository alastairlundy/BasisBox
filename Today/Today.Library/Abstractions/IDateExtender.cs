namespace Today.Library.Abstractions;

public interface IDateExtender
{
    public int GetWeekOfYear(DateTime date);

    public int GetDayOfMonth(DateTime date);
    
}