using System;
using Dissertation.Persistence.Entities.Base;

namespace Dissertation.Persistence.Entities;

/// <summary>
/// Исполнитель
/// </summary>
public class Executor : BaseModel
{
    public Guid PersonId { get; set; }
    //public virtual PerformancePlan? PerformancePlan { get; set; }
}
