namespace Dissertation.Persistence.Entities;

#nullable disable
public class PlannedResponsePlan : AuditableEntity
{
    public string Title { get; set; } = null!;
    public TimeSpan Performance { get; set; }
    public virtual List<RespondentSampleModel> RespondentSampleModels { get; set; }
    public virtual List<PathMapContent> PathMaps { get; set; } = null!;
}
