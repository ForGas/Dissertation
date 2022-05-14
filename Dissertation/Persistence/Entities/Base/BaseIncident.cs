using Dissertation.Common.Services.CSIRP;
using Dissertation.Persistence.Entities.Common;

#nullable disable
namespace Dissertation.Persistence.Entities;

public abstract class BaseIncident : AuditableEntity, IIncident
{
    public Priority Priority { get; set; }
    public string IpAddrees { get; set; }
    public string Domain { get; set; }
    public IncidentType TypeName => IncidentType.NotDefined;

    public static explicit operator BaseIncident(IncidentType incident)
    {
        return (BaseIncident)incident;
    }
}
