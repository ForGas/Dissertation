namespace Dissertation.Common.Services;

public interface IDateTime
{
    DateTime Now { get; }
    DateOnly CurrentDateOnly { get; }
    TimeOnly CurrentTimeOnly { get; }
}