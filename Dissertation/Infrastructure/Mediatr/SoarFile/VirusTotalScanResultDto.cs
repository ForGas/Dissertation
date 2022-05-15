using AutoMapper;
using Dissertation.Infrastructure.Mappings;
using Dissertation.Persistence.Entities;
using Newtonsoft.Json;

#nullable disable
namespace Dissertation.Infrastructure.Mediatr.SoarFile;

public class VirusTotalScanResultDto : IMapFrom<FileDetails>, IMapFrom<VirusTotalReportDetails>
{
    public string Md5 { get; set; }
    public string Permalink { get; set; }
    public string Resource { get; set; }

    [JsonProperty("scan_id")]
    public string ScanId { get; set; }
    public string Sha1 { get; set; }
    public string Sha256 { get; set; }

    [JsonProperty("verbose_msg")]
    public string VerboseMsg { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<VirusTotalScanResultDto, FileDetails>();
        profile.CreateMap<VirusTotalScanResultDto, VirusTotalReportDetails>();
    }
}
