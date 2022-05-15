﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Dissertation.Persistence.Entities.Common;

[JsonConverter(typeof(StringEnumConverter))]
public enum SystemScanStatus
{
    [EnumMember(Value = "Нет определения")]
    [Display(Name = "Нет определения")]
    NoDefinition = 0,

    [EnumMember(Value = "Файл чист")]
    [Display(Name = "Файл чист")]
    Clear = 1,

    [EnumMember(Value = "Файл заражен")]
    [Display(Name = "Файл заражен")]
    Virus = 2,

    [EnumMember(Value = "Требуется анализ")]
    [Display(Name = "Требуется анализ")]
    Analysis = 3,
}