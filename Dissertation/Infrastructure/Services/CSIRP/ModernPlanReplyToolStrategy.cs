using Dissertation.Common.Services;
using Dissertation.Common.Services.CSIRP;
using Dissertation.Persistence.Entities;

namespace Dissertation.Infrastructure.Services.CSIRP;

public class ModernPlanReplyToolStrategy : IPlanReplyToolStrategy
{
    private readonly IApplicationDbContext _context;
    private readonly IIncident _incident;

    public ModernPlanReplyToolStrategy(IIncident incident, IApplicationDbContext context)
        => (_incident, _context) = (incident, context);

    public IIncident GetIncident() => _incident;

    public PlannedResponsePlan GetPlan()
    {
        return new PlannedResponsePlan
        {
        };
    }

    public void Launch() => Console.WriteLine("algorithm B");
}
