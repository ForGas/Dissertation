using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Dissertation.Persistence.Entities.Common;

///<summary>Приоритет инцидента</summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum Priority
{
    Low,
    Middle,
    High
}
