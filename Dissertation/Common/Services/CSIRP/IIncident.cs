using Dissertation.Persistence.Entities.Common;

namespace Dissertation.Common.Services.CSIRP;

///<summary>Интерфейс инцидента по безопаности</summary>
public interface IIncident 
{
    IncidentType TypeName { get; }
    Priority Priority { get; set; }
    string Domain { get; set; }
    string IpAddrees { get; set; }
}
