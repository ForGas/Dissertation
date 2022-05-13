using Dissertation.Persistence.Entities.Common;

namespace Dissertation.Persistence.Entities;

///<summary>Файловый инцидент</summary>
public class FileIncident : BaseIncident
{
    public FileIncident()
    {

    }

    public new IncidentType TypeName => IncidentType.File;
    public string FileName { get; set; } = null!;
    public string FolderName { get; set; } = null!;
    public string FullPath { get; set; } = null!;
}
