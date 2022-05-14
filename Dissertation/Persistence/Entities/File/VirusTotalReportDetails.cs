namespace Dissertation.Persistence.Entities;

public class VirusTotalReportDetails : AuditableEntity
{
    public virtual FileDetails BasicDetails { get; set; } = null!;
}
