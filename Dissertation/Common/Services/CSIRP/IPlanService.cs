using Dissertation.Infrastructure.Services.CSIRP;
using Dissertation.Persistence.Entities;

namespace Dissertation.Common.Services.CSIRP;

public interface IPlanService
{
    Plan GetPlan(PlanTypeStrategy type, IIncident incident);
    IPlanReplyToolStrategy GetPlanReplyToolStrategy(PlanTypeStrategy type, IIncident incident);
}

