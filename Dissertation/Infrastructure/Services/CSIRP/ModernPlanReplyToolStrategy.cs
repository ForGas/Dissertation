using Dissertation.Common.Services.CSIRP;
using Dissertation.Persistence.Entities;

namespace Dissertation.Infrastructure.Services.CSIRP;

public class ModernPlanReplyToolStrategy : IPlanReplyToolStrategy
{
    private readonly IIncident _incident;

    public ModernPlanReplyToolStrategy(IIncident incident)
        => _incident = incident;

    public IIncident GetIncident() => _incident;

    public PlannedResponsePlan GetPlan()
    {
        return new PlannedResponsePlan
        {
        };
    }

    public void Launch() => Console.WriteLine("algorithm B");
}
