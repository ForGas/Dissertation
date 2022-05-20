using Dissertation.Persistence.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dissertation.Persistence.Entities;

public class NetworkIncident : BaseIncident
{
    [NotMapped]
    public override IncidentType TypeName => IncidentType.Network;
}