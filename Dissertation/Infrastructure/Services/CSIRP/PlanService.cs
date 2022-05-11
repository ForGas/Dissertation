using Dissertation.Common.Services.CSIRP;
using Dissertation.Persistence.Entities;

namespace Dissertation.Infrastructure.Services.CSIRP;

public class PlanService : IPlanService
{
    public Plan GetPlan(PlanTypeStrategy type, IIncident incident)
        => PlanStrategyFactory
            .GetStrategy(type, incident)
            .GetPlan();
    public IPlanReplyToolStrategy GetPlanReplyToolStrategy(PlanTypeStrategy type, IIncident incident)
        => PlanStrategyFactory.GetStrategy(type, incident);
}

public class PlanStrategyFactory
{
    private static IDictionary<PlanTypeStrategy, Func<IIncident, IPlanReplyToolStrategy>>
      _strategies = new Dictionary<PlanTypeStrategy, Func<IIncident, IPlanReplyToolStrategy>>()
      {
      { PlanTypeStrategy.Pattern, (incident) => new PatternPlanReplyToolStrategy(incident) },
      { PlanTypeStrategy.Modern, (incident) => new ModernPlanReplyToolStrategy(incident) }
      };

    public static IPlanReplyToolStrategy GetStrategy(PlanTypeStrategy type, IIncident incident)
        => _strategies[type](incident);
}
