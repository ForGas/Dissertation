using Dissertation.Persistence.Entities;

namespace Dissertation.Common.Services.CSIRP;

public interface IPlanReplyToolStrategy
{
    Plan GetPlan();
    void Launch();
    IIncident GetIncident();
}






