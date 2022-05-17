using Dissertation.Infrastructure.Services.CSIRP;
using Dissertation.Persistence.Entities;

namespace Dissertation.Common.Services.CSIRP;

///<summary>Computer Security Incident Response Plan</summary>
public interface IComputerSecurityIncidentResponsePlan<TIncident>
    where TIncident : IIncident
{
    bool GetApprove();
    void SetStrategyFactory(IPlanService strategy);
    PlannedResponsePlan GetPlanFactory(PlanTypeStrategy type, TIncident incident);
    IPlanReplyToolStrategy GetPlanReplyTool(PlanTypeStrategy type, TIncident incident);
}
