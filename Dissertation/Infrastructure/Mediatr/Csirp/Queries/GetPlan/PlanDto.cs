using AutoMapper;
using Dissertation.Infrastructure.Mappings;
using Dissertation.Persistence.Entities;
using Dissertation.Persistence.Entities.Common;

namespace Dissertation.Infrastructure.Mediatr.Csirp.Queries.GetPlan;

public class PlanDto : IMapFrom<PlannedResponsePlan>, IMapFrom<RespondentJobSample>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public TimeSpan Performance { get; set; }
    public PlanTypeStrategy Type { get; set; }
    public Stage Stage { get; set; }
    public Priority Priority { get; set; }
    public IncidentType IncidentType { get; set; }
    public string PlanUsageInformation { get; set; } = null!;
    public List<PathMapContentDto> PathMaps { get; set; } = new();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PlanDto, PlannedResponsePlan>();
        profile.CreateMap<PlanDto, RespondentJobSample>();
    }
}

public class PathMapContentDto : IMapFrom<PathMapContent>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public PathMapStage Stage { get; set; }
    public string Source { get; set; } = null!;
    public string ResponseToolInfo { get; set; } = null!;
    public string Description { get; set; } = null!;

    public string PlanUsageInformation { get; set; } = null!;
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PlanDto, PlannedResponsePlan>();
        profile.CreateMap<PlanDto, RespondentJobSample>();
    }
}

