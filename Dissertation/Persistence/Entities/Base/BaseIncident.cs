using Dissertation.Common.Services.CSIRP;
using Dissertation.Persistence.Entities.Common;

#nullable disable
namespace Dissertation.Persistence.Entities;

public abstract class BaseIncident : AuditableEntity, IIncident
{
    public Priority Priority { get; set; } = Priority.Low;
    public string IpAddrees { get; set; }
    public string Domain { get; set; }
    public IncidentType TypeName => IncidentType.NotDefined;
    public virtual PlannedResponsePlan Plan { get; set; }

    public static explicit operator BaseIncident(IncidentType incident)
    {
        return (BaseIncident)incident;
    }
}
