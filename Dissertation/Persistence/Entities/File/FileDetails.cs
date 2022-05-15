namespace Dissertation.Persistence.Entities;

public class FileDetails : AuditableEntity
{
    public Guid FileIncidentId { get; set; }
    public string Md5 { get; set; } = null!;
    public string Sha1 { get; set; } = null!;
    public string Sha256 { get; set; } = null!;
    public virtual FileIncident Incident { get; set; } = null!;
    public virtual VirusTotalReportDetails? Report { get; set; } = null!;
}
