using Dissertation.Common.Services;
using Dissertation.Common.Services.AutomationLogic;
using Dissertation.Common.Services.CSIRP;
using Dissertation.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dissertation.Infrastructure.Services;

public class AutomateStaffStatisticWorkloadJobService
{
    private readonly IApplicationDbContext _context;
    private readonly IComputerSecurityIncidentResponsePlan<IIncident> _incidentResponsePlan;
    private readonly IRespondentAutomationLogic _respondentAutomationLogic;

    public AutomateStaffStatisticWorkloadJobService(
            IApplicationDbContext context,
            IComputerSecurityIncidentResponsePlan<IIncident> incidentResponsePlan,
            IRespondentAutomationLogic respondentAutomationLogic
        ) => (_context, _incidentResponsePlan, _respondentAutomationLogic) = (context, incidentResponsePlan, respondentAutomationLogic);

    public async Task AutomateAsync()
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var incidents = _context.FileIncidents.Include(x => x.JobSample)
                .Where(x => x.JobSample == null)
                .ToList();

            foreach (var incident in incidents)
            {
                var plan = _incidentResponsePlan.GetPlan(PlanTypeStrategy.Pattern, incident);
                var priorityQueue = _respondentAutomationLogic.GetPriorityQueueWorkloadStatistic(incident);

                var cyberSecuritySpecialistStatisticId = priorityQueue.UnorderedItems
                    .FirstOrDefault(x => x.Element.Item2 == StaffType.CyberSecuritySpecialist)
                    .Element.Item1;

                var analystStatisticId = priorityQueue.UnorderedItems
                    .FirstOrDefault(x => x.Element.Item2 == StaffType.Analyst)
                    .Element.Item1;

                var staffStatistics = _context.StaffStatistics
                    .Where(x => x.Id == cyberSecuritySpecialistStatisticId || x.Id == analystStatisticId)
                    .ToList();

                var respondentJobSamples = _context.RespondentJobSamples
                    .Where(x => x.PlannedResponsePlan!.Id == plan.Id)
                    .OrderBy(x => x.Created)
                    .ToList()
                    .LastOrDefault();

                respondentJobSamples.StaffStatistics.AddRange(staffStatistics);
            }

            await _context.SaveChangesAsync(new CancellationToken());
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
