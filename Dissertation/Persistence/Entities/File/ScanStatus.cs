using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Dissertation.Persistence.Entities;

[JsonConverter(typeof(StringEnumConverter))]
public enum ScanStatus
{
    [EnumMember(Value = "Нет определения")]
    [Display(Name = "Нет определения")]
    NoDefinition = 0,

    [EnumMember(Value = "В процессе анализа")]
    [Display(Name = "В процессе анализа")]
    Analysis = 1,

    [EnumMember(Value = "Файл чист")]
    [Display(Name = "Файл чист")]
    Clean = 2,

    [EnumMember(Value = "Файл заражен")]
    [Display(Name = "Файл заражен")]
    Virus = 3,
}
