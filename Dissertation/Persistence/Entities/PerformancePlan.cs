namespace Dissertation.Persistence.Entities;

///<summary>Информация о плане выполнения</summary>
public class PerformancePlan : AuditableEntity
{
    public string? Name { get; set; }
    public Guid? IncidentId { get; set; }
    //public virtual List<Executor>? Executors { get; set; }
}
