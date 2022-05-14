using Dissertation.Persistence.Entities.Base;

namespace Dissertation.Persistence.Entities;

public class AuditableEntity : BaseIdentity
{
    public DateTime Created { get; set; }
    public DateTime? LastModified { get; set; }
}
