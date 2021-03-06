using Dissertation.Persistence.Entities.Common;

namespace Dissertation.Common.Services.CSIRP;

///<summary>Интерфейс инцидента по безопаности</summary>
public interface IIncident 
{
    Guid Id { get; init; }
    IncidentType Type { get; }
    Priority Priority { get; set; }
    string Domain { get; set; }
    string IpAddress { get; set; }
}
