using Dissertation.Common.Services;
using Dissertation.Common.Services.CSIRP;
using Dissertation.Persistence.Entities;
using Dissertation.Persistence.Entities.Common;
using Microsoft.EntityFrameworkCore;

#nullable disable
namespace Dissertation.Infrastructure.Services.CSIRP;

public class PatternPlanReplyToolStrategy : IPlanReplyToolStrategy
{
    private readonly IApplicationDbContext _context;
    private readonly IIncident _incident;

    public PatternPlanReplyToolStrategy(IIncident incident, IApplicationDbContext context)
        => (_incident, _context) = (incident, context);

    public IIncident GetIncident() => _incident;

    public async Task<PlannedResponsePlan> GetPlanAsync()
    {
        var plan = _context.PlannedResponsePlans.Include(x => x.PathMaps)
            .Where(x => x.Type == PlanTypeStrategy.Pattern && x.IncidentType == _incident.Type
                && x.Priority == _incident.Priority)
            .FirstOrDefault();

        var respondentJobSample = new RespondentJobSample
        {
            Stage = Stage.InAcceptance,
            PlanUsageInformation = "Default",
        };

        switch (_incident.Type)
        {
            case IncidentType.File:
                respondentJobSample.FileIncident = (FileIncident)_incident;
                break;
            case IncidentType.Network:
                respondentJobSample.NetworkIncident = (NetworkIncident)_incident;
                break;
        }

        _context.RespondentJobSamples.Add(respondentJobSample);
        plan.RespondentJobSamples.Add(respondentJobSample);

        _ = await _context.SaveChangesAsync(new CancellationToken());

        return plan;
    }

    public void Launch() => Console.WriteLine("algorithm A");
}
