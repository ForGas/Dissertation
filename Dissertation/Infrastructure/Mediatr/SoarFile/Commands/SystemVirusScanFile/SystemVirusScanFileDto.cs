using AutoMapper;
using Dissertation.Infrastructure.Mappings;
using Dissertation.Persistence.Entities;

namespace Dissertation.Infrastructure.Mediatr.SoarFile.Commands.SystemVirusScanFile;

public class SystemVirusScanFileDto : IMapFrom<FileIncident>
{
    public Guid Id { get; set; }
    public ScanStatus Status { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<FileIncident, SystemVirusScanFileDto>()
            .ForMember(x => x.Status, opt => opt.MapFrom(y => y.Status));
    }
}
