namespace Common;

public class DateTimeService : IDateTimeService
{
    public DateTime Now => DateTime.UtcNow;
}