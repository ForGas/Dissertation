using Dissertation.Persistence.Entities.Base;

namespace Dissertation.Persistence.Entities;

public class AuditableEntity : BaseModel
{
    public DateTime Created { get; set; }
    public DateTime? LastModified { get; set; }
}
