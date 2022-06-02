using Dissertation.Persistence.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dissertation.Persistence.Entities;

public class FileIncident : BaseIncident
{
    [NotMapped]
    public override IncidentType Type => IncidentType.File;
    public ScanStatus Status { get; set; }
    public string FileName { get; set; } = null!;
    public string FolderName { get; set; } = null!;
    public string FullPath { get; set; } = null!;
    public bool IsSystemScanClean { get; set; }
    public bool IsVirusHashInfoClean { get; set; }
    public virtual FileDetails? Details { get; set; }
}
