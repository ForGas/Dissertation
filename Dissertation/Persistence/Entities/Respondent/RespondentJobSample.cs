namespace Dissertation.Persistence.Entities;

public class RespondentJobSample : AuditableEntity
{
    public Stage Stage { get; set; }
    public string PlanUsageInformation { get; set; } = null!;
    public Guid? IncidentId { get; set; }
    public Guid? PlannedResponsePlanId { get; set; }
    public virtual PlannedResponsePlan? PlannedResponsePlan { get; set; }
    public virtual FileIncident? FileIncident { get; set; }
    public virtual NetworkIncident? NetworkIncident { get; set; }
    public virtual StaffStatistic? StaffStatistic { get; set; }
}

public enum Stage
{
    InAcceptance = 0,
    InProgress = 1,
    Completed = 2,
}
