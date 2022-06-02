using Newtonsoft.Json;

namespace Dissertation.Infrastructure.Mediatr.SoarFile;

public class VirusScanReportDto
{
    public string Name { get; set; } = string.Empty;

    [JsonProperty("detected")]
    public bool Detected { get; set; }

    [JsonProperty("result")]
    public string Result { get; set; } = string.Empty;
}
