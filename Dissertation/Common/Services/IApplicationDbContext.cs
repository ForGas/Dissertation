using Dissertation.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dissertation.Common.Services;

public interface IApplicationDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    DbSet<Analyst> Analysts { get; set; }
    DbSet<CyberSecuritySpecialist> CyberSecuritySpecialists { get; set; }
    DbSet<FileIncident> Executors { get; set; }
    //DbSet<PatternePlan> PatternePlans { get; set; }
    //DbSet<Plan> Plans { get; set; }
    //DbSet<ResponseToolInfo> ResponseToolInfo { get; set; }
}
