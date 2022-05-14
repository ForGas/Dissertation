namespace Dissertation.Persistence.Entities;

public class FileDetails : AuditableEntity
{
    public virtual FileIncident Incident { get; set; } = null!;
    public string Sha256 { get; set; } = null!;
    public virtual VirusTotalReportDetails? Report { get; set; } = null!;
}
