using Dissertation.Common.Services.CSIRP;
using Dissertation.Persistence.Entities;

namespace Dissertation.Infrastructure.Services.CSIRP;

public class PatternPlanReplyToolStrategy : IPlanReplyToolStrategy
{
    private readonly IIncident _incident;

    public PatternPlanReplyToolStrategy(IIncident incident)
        => _incident = incident;

    public IIncident GetIncident() => _incident;

    public Plan GetPlan()
    {
        return new Plan
        {
            PathMap = "a1->a2->b3",
            Duration = DateTime.UtcNow.AddHours(1) - DateTime.UtcNow
        };
    }

    public void Launch() => Console.WriteLine("algorithm A");
}
