using MediatR;
using AutoMapper;
using Dissertation.Common.Services;
using Microsoft.EntityFrameworkCore;
using Dissertation.Common.Services.CSIRP;
using Dissertation.Infrastructure.Services;
using Dissertation.Persistence.Entities;

namespace Dissertation.Infrastructure.Mediatr.Csirp.Queries.GetPlan;

public record class GetPlanQuery(Guid IncidentId) : IRequest<PlanDto>;

#nullable disable
public class GetPlanQueryHandler : IRequestHandler<GetPlanQuery, PlanDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IComputerSecurityIncidentResponsePlan<IIncident> _incidentResponsePlan;
    private readonly IRespondentAutomationLogic _respondentAutomationLogic;

    public GetPlanQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IComputerSecurityIncidentResponsePlan<IIncident> incidentResponsePlan,
            IRespondentAutomationLogic respondentAutomationLogic
        ) => (_context, _mapper, _incidentResponsePlan, _respondentAutomationLogic) 
            = (context, mapper, incidentResponsePlan, respondentAutomationLogic);

    public async Task<PlanDto> Handle(GetPlanQuery request, CancellationToken cancellationToken)
    {
        var incident = await _context.FileIncidents.Include(x => x.JobSample)
            .Where(x => x.JobSample == null && x.Id == request.IncidentId)
            .FirstOrDefaultAsync();

        var plan =_incidentResponsePlan.GetPlan(PlanTypeStrategy.Pattern, incident);
        var priorityQueue = _respondentAutomationLogic.GetPriorityQueueWorkloadStatistic(incident);

        var cyberSecuritySpecialistStatisticId = priorityQueue.UnorderedItems
            .FirstOrDefault(x => x.Element.Item2 == StaffType.CyberSecuritySpecialist)
            .Element.Item1;

        var analystStatisticId = priorityQueue.UnorderedItems
            .FirstOrDefault(x => x.Element.Item2 == StaffType.Analyst)
            .Element.Item1;

        var staffStatistics = _context.StaffStatistics
            .Where(x => x.Id == cyberSecuritySpecialistStatisticId || x.Id == analystStatisticId)
            .ToList();

        var respondentJobSamples = _context.RespondentJobSamples
            .Where(x => x.PlannedResponsePlan!.Id == plan.Id)
            .OrderBy(x => x.Created)
            .ToList()
            .LastOrDefault();

        respondentJobSamples.StaffStatistics.AddRange(staffStatistics);
        _ = await _context.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<PlanDto>(plan);
        var temp = _mapper.Map<PlanDto>(respondentJobSamples);
        result.Stage = temp.Stage;

        return result;
    }
}
