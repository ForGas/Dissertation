namespace Dissertation.Common.Services.CSIRP;

public abstract class BaseIncidentResult<T> : IIncidentResult<T>
    where T : IPlan
{
    protected readonly T? Value;
    protected readonly string? Error;

    protected BaseIncidentResult(T value) => Value = value;

    protected BaseIncidentResult(string error) => Error = error;

    protected bool HasValue => Error is null;

    public T? GetPlan() => HasValue ? Value : default;


    public static implicit operator bool(BaseIncidentResult<T> result) => result.HasValue;
}
