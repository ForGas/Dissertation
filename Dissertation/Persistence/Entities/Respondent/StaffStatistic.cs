using Dissertation.Persistence.Entities.Common;

namespace Dissertation.Persistence.Entities;

public class StaffStatistic : AuditableEntity
{
    public Guid RespondentId { get; set; }
    public bool IsRelevance { get; set; }
    public IncidentType StatisticsType { get; set; }
    public Workload Workload { get; set; }
    public Guid StaffId { get; set; }
    public virtual Staff Staff { get; set; } = null!;
    public virtual List<RespondentJobSample> JobSamples { get; set; } = new();
}
