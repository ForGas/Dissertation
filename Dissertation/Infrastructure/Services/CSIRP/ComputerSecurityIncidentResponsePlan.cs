using Dissertation.Common.Services.CSIRP;
using Dissertation.Persistence.Entities;

namespace Dissertation.Infrastructure.Services.CSIRP;

public class ComputerSecurityIncidentResponsePlan : IComputerSecurityIncidentResponsePlan<IIncident>
{
    private IPlanService _planService;

    protected bool _isApproval { get; set; } = default;
    protected readonly DeterminingScope _determiningScope;

    public ComputerSecurityIncidentResponsePlan(IPlanService planService)
        => (_planService) = (planService);

    public virtual bool GetApprove() => _isApproval;

    public void SetStrategyFactory(IPlanService strategy)
        => (_planService) = (strategy);

    public PlannedResponsePlan GetPlan(PlanTypeStrategy type, IIncident incident)
        => _planService.GetPlanFactory(type, incident);

    public IPlanReplyToolStrategy GetPlanReplyTool(PlanTypeStrategy type, IIncident incident)
        => _planService.GetPlanReplyToolStrategy(type, incident);
}
