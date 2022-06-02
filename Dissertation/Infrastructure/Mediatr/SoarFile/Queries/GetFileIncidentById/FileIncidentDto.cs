using AutoMapper;
using Dissertation.Infrastructure.Mappings;
using Dissertation.Persistence.Entities;
using Dissertation.Persistence.Entities.Common;

namespace Dissertation.Infrastructure.Mediatr.SoarFile.Queries.GetFileIncidentById;

public class FileIncidentDto : IMapFrom<FileIncident>
{
    public Guid Id { get; set; }
    public ScanStatus Status { get; set; }
    public bool IsSystemScanClean { get; set; }
    public string FileName { get; set; } = null!;
    public IncidentType TypeName { get; set; }
    public string Md5 { get; set; } = null!;
    public string Sha1 { get; set; } = null!;
    public string Sha256 { get; set; } = null!;
    public Priority Priority { get; set; }
    public string IpAddress { get; set; } = string.Empty;
    public string Domain { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Report { get; set; } = string.Empty;
    public string ScanId { get; set; } = null!;
    public string Resource { get; set; } = null!;
    public Guid VirusTotalReportId { get; set; }
    public DateTime Created { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<FileIncident, FileIncidentDto>()
            .ForMember(x => x.Status, opt => opt.MapFrom(y => y.Status))
            .ForMember(x => x.Code, opt => opt.NullSubstitute(string.Empty))
            .ForMember(x => x.IpAddress, opt => opt.NullSubstitute(string.Empty))
            .ForMember(x => x.Domain, opt => opt.NullSubstitute(string.Empty))
            .ForMember(x => x.TypeName, opt => opt.MapFrom(y => y.Type))
            .ForMember(x => x.Md5, opt => opt.MapFrom(y => y.Details!.Md5))
            .ForMember(x => x.Sha1, opt => opt.MapFrom(y => y.Details!.Sha1))
            .ForMember(x => x.Sha256, opt => opt.MapFrom(y => y.Details!.Sha256))
            .ForMember(x => x.Resource, opt => opt.MapFrom(y => y.Details!.Report!.Resource))
            .ForMember(x => x.Created, opt => opt.MapFrom(y => y.Details!.Report!.Created))
            .ForMember(x => x.Report, opt => opt.MapFrom(y => y.Details!.Report!.JsonContent))
            .ForMember(x => x.ScanId, opt => opt.MapFrom(y => y.Details!.Report!.ScanId))
            .ForMember(x => x.VirusTotalReportId, opt => opt.MapFrom(y => y.Details!.Report!.Id))
            ;
    }
}
