using Dissertation.Persistence.Entities.Common;

namespace Dissertation.Persistence.Entities;

///<summary>Файловый инцидент</summary>
public class FileIncident : BaseIncident
{
    public FileIncident()
    {

    }

    public new IncidentType TypeName => IncidentType.File;
    public string FileName { get; set; } = string.Empty;
    public string FolderName { get; set; } = string.Empty;
    public string FullPath { get; set; } = string.Empty;
}
