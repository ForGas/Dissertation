using Dissertation.Common.Services.CSIRP;
using Dissertation.Persistence.Entities;

namespace Dissertation.Common.Services.AutomationLogic;

public interface IRespondentAutomationLogic : IBaseDecisionTreeLogic
{
    PriorityQueue<(Guid, StaffType), Workload> GetPriorityQueueWorkloadStatistic(IIncident incident);
}
