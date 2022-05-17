namespace Dissertation.Persistence.Entities;

public class StaffStatistic : AuditableEntity
{
    public Guid RespondentId { get; set; }
    public int СurrentTaskCount { get; set; }
    public int CompletedTaskCount { get; set; }
    public bool IsRelevance { get; set; }
    public StatisticsType StatisticsType { get; set; }
    public Workload Workload { get; set; }
    public Guid StaffId { get; set; }
    public virtual Staff Staff { get; set; } = null!;
}
