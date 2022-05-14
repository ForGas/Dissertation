using Dissertation.Persistence.Entities.Common;

namespace Dissertation.Persistence.Entities;

public class FileIncident : BaseIncident
{
    public SystemScanStatus Status { get; set; }
    public new IncidentType TypeName => IncidentType.File;
    public string FileName { get; set; } = null!;
    public string FolderName { get; set; } = null!;
    public string FullPath { get; set; } = null!;
    public virtual FileDetails? Details { get; set; }
}
