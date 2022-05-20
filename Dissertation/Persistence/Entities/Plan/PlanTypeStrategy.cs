using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Dissertation.Persistence.Entities;

[JsonConverter(typeof(StringEnumConverter))]
public enum PlanTypeStrategy
{
    Pattern = 0,
    Modern = 1,
}

