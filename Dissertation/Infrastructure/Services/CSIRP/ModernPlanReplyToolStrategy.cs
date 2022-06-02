using Dissertation.Common.Services;
using Dissertation.Common.Services.CSIRP;
using Dissertation.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

#nullable disable
namespace Dissertation.Infrastructure.Services.CSIRP;

public class ModernPlanReplyToolStrategy : IPlanReplyToolStrategy
{
    private readonly IApplicationDbContext _context;
    private readonly IIncident _incident;

    public ModernPlanReplyToolStrategy(IIncident incident, IApplicationDbContext context)
        => (_incident, _context) = (incident, context);

    public IIncident GetIncident() => _incident;

    public async Task<PlannedResponsePlan> GetPlanAsync()
    {
        var plan = await _context.PlannedResponsePlans
            .Where(x => x.Type == PlanTypeStrategy.Modern && x.IncidentType == _incident.Type
                && x.Priority == _incident.Priority)
            .FirstOrDefaultAsync();

        return plan;
    }

    public void Launch() => Console.WriteLine("algorithm B");
}
