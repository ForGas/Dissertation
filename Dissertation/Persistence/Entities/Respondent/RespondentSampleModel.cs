namespace Dissertation.Persistence.Entities;

#nullable disable
public class RespondentSampleModel : AuditableEntity
{
    public RespondentStage Stage { get; set; }
    public string PlanUsageInformation { get; set; }
    public Guid PlannedResponsePlanId { get; set; }
    public Guid IncidentId { get; set; }
    public virtual PlannedResponsePlan Plan { get; set; }
    public virtual List<Staff> Staffs { get; set; }
}

public enum RespondentStage
{

}
