namespace Dissertation.Persistence.Entities.Base;

public abstract class BaseIdentity
{
    public Guid Id { get; init; } = Guid.NewGuid();
}
