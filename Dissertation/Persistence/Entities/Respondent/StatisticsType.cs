using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Dissertation.Persistence.Entities;

[JsonConverter(typeof(StringEnumConverter))]
public enum StatisticsType
{
    File = 1,
    Network = 2,
}
