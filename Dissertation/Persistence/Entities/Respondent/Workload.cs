using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Dissertation.Persistence.Entities;

[JsonConverter(typeof(StringEnumConverter))]
public enum Workload
{
    Neutral = 0,
    Low = 1,
    Normal = 2,
    High = 3,
    Exceed = 4,
    Critical = 5,
}
