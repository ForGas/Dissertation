using Dissertation.Persistence.Entities;

namespace Dissertation.Common.Services.CSIRP;

public interface IPlanService
{
    PlannedResponsePlan GetPlanFactory(PlanTypeStrategy type, IIncident incident);
    IPlanReplyToolStrategy GetPlanReplyToolStrategy(PlanTypeStrategy type, IIncident incident);
}

