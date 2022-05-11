using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Dissertation.Persistence.Entities.Common;

[JsonConverter(typeof(StringEnumConverter))]
public enum IncidentType
{
    NotDefined = 0,
    Analysis = 1,
    File = 2,
    Network = 3,
}
