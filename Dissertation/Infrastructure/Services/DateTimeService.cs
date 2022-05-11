using Dissertation.Common.Services;

namespace Dissertation.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
    public DateOnly CurrentDateOnly => DateOnly.FromDateTime(DateTime.Now);
    public TimeOnly CurrentTimeOnly => TimeOnly.FromDateTime(DateTime.Now);
}
