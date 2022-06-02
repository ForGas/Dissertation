using Dissertation.Common.Services;
using Dissertation.Common.Services.CSIRP;
using Dissertation.Persistence.Entities;

namespace Dissertation.Infrastructure.Services.CSIRP;

public class PlanService : IPlanService
{
    private readonly IApplicationDbContext _context;
    public PlanService(IApplicationDbContext context)
        => (_context) = (context);

    public PlannedResponsePlan GetPlanFactory(PlanTypeStrategy type, IIncident incident)
        => PlanStrategyFactory
            .GetStrategy(type, incident, _context)
            .GetPlanAsync().Result;
    public IPlanReplyToolStrategy GetPlanReplyToolStrategy(PlanTypeStrategy type, IIncident incident)
        => PlanStrategyFactory.GetStrategy(type, incident, _context);
}

public class PlanStrategyFactory
{
    private static IDictionary<PlanTypeStrategy, Func<IIncident, IApplicationDbContext, IPlanReplyToolStrategy>>
      _strategies = new Dictionary<PlanTypeStrategy, Func<IIncident, IApplicationDbContext, IPlanReplyToolStrategy>>()
      {
      { PlanTypeStrategy.Pattern, (incident, context) => new PatternPlanReplyToolStrategy(incident, context) },
      { PlanTypeStrategy.Modern, (incident, context) => new ModernPlanReplyToolStrategy(incident, context) }
      };

    public static IPlanReplyToolStrategy GetStrategy(PlanTypeStrategy type, IIncident incident, IApplicationDbContext context)
        => _strategies[type](incident, context);
}