namespace Dissertation.Persistence.Entities;

public class PathMapContent : AuditableEntity
{
    public string Title { get; set; } = null!;
    public PathMapStage Stage { get; set; }
    public string Source { get; set; } = null!;
    public string ResponseToolInfo { get; set; } = null!;
    public string Description { get; set; } = null!;
    public virtual List<PlannedResponsePlan> Plans { get; set; } = new();
}
