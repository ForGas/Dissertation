using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Dissertation.Persistence.Entities;

[JsonConverter(typeof(StringEnumConverter))]
public enum StaffType
{
    CyberSecuritySpecialist,
    Analyst,
    ServiceManager,
    Admin,
    Director,
}
