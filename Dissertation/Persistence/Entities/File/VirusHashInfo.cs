namespace Dissertation.Persistence.Entities;

#nullable disable
public class VirusHashInfo : AuditableEntity
{
    public string Title { get; set; }
    public string Source { get; set; }
    public string Md5 { get; set; }
    public string Sha1 { get; set; }
    public string Sha256 { get; set; }
    public bool IsVirus { get; set; }
}
