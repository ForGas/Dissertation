namespace Dissertation.Persistence.Entities.Base;

public abstract class BaseModel
{
    public Guid Id { get; init; } = Guid.NewGuid();
}
