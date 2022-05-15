namespace Dissertation.Persistence.Entities;

public class VirusTotalReportDetails : AuditableEntity
{
    public Guid FileDetailsId { get; set; }
    public virtual FileDetails FileDetails { get; set; } = null!;
    public string ScanId { get; set; } = null!;
    public string Resource { get; set; } = null!;
    public string Permalink { get; set; } = null!;
    public string? JsonContent { get; set; }
}
