using Dissertation.Common.Services.CSIRP;
using Dissertation.Persistence.Entities.Common;

#nullable disable
namespace Dissertation.Persistence.Entities;

public abstract class BaseIncident : AuditableEntity, IIncident
{
    public Priority Priority { get; set; } = Priority.Low;
    public string IpAddress { get; set; }
    public string Domain { get; set; }
    public string Code { get; set; }
    public virtual RespondentJobSample JobSample { get; set; }
    public virtual IncidentType TypeName => IncidentType.NotDefined;

    public static explicit operator BaseIncident(IncidentType incident)
    {
        return (BaseIncident)incident;
    }
}
