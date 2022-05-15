using Dissertation.Persistence.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dissertation.Persistence.Entities;

public class FileIncident : BaseIncident
{
    [NotMapped]
    public new IncidentType TypeName => IncidentType.File;
    public SystemScanStatus Status { get; set; }
    public string FileName { get; set; } = null!;
    public string FolderName { get; set; } = null!;
    public string FullPath { get; set; } = null!;
    public virtual FileDetails? Details { get; set; }
}
