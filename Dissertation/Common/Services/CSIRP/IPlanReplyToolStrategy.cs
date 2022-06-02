using Dissertation.Persistence.Entities;

namespace Dissertation.Common.Services.CSIRP;

public interface IPlanReplyToolStrategy
{
    Task<PlannedResponsePlan> GetPlanAsync();
    void Launch();
    IIncident GetIncident();
}






