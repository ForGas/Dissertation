using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Dissertation.Persistence.Entities;

[JsonConverter(typeof(StringEnumConverter))]
public enum PathMapStage
{
    Initial = 0,
    Define = 1,
    Analysis = 2,
    Escalate = 3,
    Complete = 4,
}
