using Dissertation.Persistence.Entities;

namespace Dissertation.Common.Services.CSIRP;

public interface IPlanReplyToolStrategy
{
    PlannedResponsePlan GetPlan();
    void Launch();
    IIncident GetIncident();
}






