using Dissertation.Common.Services.CSIRP;
using Dissertation.Persistence.Entities.Common;

namespace Dissertation.Persistence.Entities;

/// <summary>Базовый класс сущности инцидента</summary>
public abstract class BaseIncident : AuditableEntity, IIncident
{
    public Priority Priority { get; set; }
    public string IpAddrees { get; set; } = string.Empty;
    public string Domain { get; set; } = string.Empty;
    public IncidentType TypeName => IncidentType.NotDefined;

    public static explicit operator BaseIncident(IncidentType incident)
    {
        return (BaseIncident)incident;
    }
}
