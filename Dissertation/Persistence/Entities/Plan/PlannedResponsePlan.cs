using Dissertation.Persistence.Entities.Common;

namespace Dissertation.Persistence.Entities;

#nullable disable
public class PlannedResponsePlan : AuditableEntity
{
    public string Title { get; set; } = null!;
    public TimeSpan Performance { get; set; }
    public PlanTypeStrategy Type { get; set; }
    public IncidentType IncidentType { get; set; }
    public Priority Priority { get; set; }
    public virtual List<PathMapContent> PathMaps { get; set; } = new();
    public virtual List<RespondentJobSample> RespondentJobSamples { get; set; } = new();
}
