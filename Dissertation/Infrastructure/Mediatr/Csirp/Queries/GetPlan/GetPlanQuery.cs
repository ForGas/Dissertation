using Dissertation.Common.Services;
using Dissertation.Common.Services.CSIRP;
using Dissertation.Persistence.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dissertation.Infrastructure.Mediatr.Csirp.Queries.GetPlan;

public class GetPlanQuery : IRequest
{
}

public class GetPlanQueryHandler : IRequestHandler<GetPlanQuery, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IComputerSecurityIncidentResponsePlan<IIncident> _incidentResponsePlan;

    public GetPlanQueryHandler(
            IApplicationDbContext context,
            IComputerSecurityIncidentResponsePlan<IIncident> incidentResponsePlan
        ) => (_context, _incidentResponsePlan) = (context, incidentResponsePlan);

    public async Task<Unit> Handle(GetPlanQuery request, CancellationToken cancellationToken)
    {
        var incident = await _context.FileIncidents.FirstOrDefaultAsync();

        var plan =_incidentResponsePlan.GetPlan(PlanTypeStrategy.Pattern, incident);
        var planReplyTool = _incidentResponsePlan.GetPlanReplyTool(PlanTypeStrategy.Pattern, incident);

        throw new NotImplementedException();
    }
}
