using AutoMapper;
using Dissertation.Infrastructure.Mappings;
using Dissertation.Persistence.Entities;

namespace Dissertation.Infrastructure.Mediatr.SoarFile.Queries.GetReports;

public class ReportDto : IMapFrom<VirusTotalReportDetails>
{
    public Guid Id { get; set; }
    public Guid FileDetailsId { get; set; }
    public Guid FileIncidentId { get; set; }
    public string Md5 { get; set; } = null!;
    public string Sha1 { get; set; } = null!;
    public string Sha256 { get; set; } = null!;
    public DateTime Created { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<VirusTotalReportDetails, ReportDto>()
            .ForMember(x => x.Md5, opt => opt.MapFrom(y => y.FileDetails!.Md5))
            .ForMember(x => x.Sha1, opt => opt.MapFrom(y => y.FileDetails!.Sha1))
            .ForMember(x => x.Sha256, opt => opt.MapFrom(y => y.FileDetails!.Sha256))
            .ForMember(x => x.FileIncidentId, opt => opt.MapFrom(y => y.FileDetails!.FileIncidentId))
            ;
    }
}
