using Dissertation.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Dissertation.Common.Services;

public interface IApplicationDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    DatabaseFacade Database { get; }

    #region File
    public DbSet<FileIncident> FileIncidents { get; set; }
    public DbSet<FileDetails> FileDetails { get; set; }
    public DbSet<VirusTotalReportDetails> VirusTotalReportDetails { get; set; }
    public DbSet<VirusHashInfo> VirusHashInfo { get; set; }
    #endregion

    #region Network
    public DbSet<NetworkIncident> NetworkIncidents { get; set; }
    #endregion

    #region Plan
    public DbSet<PathMapContent> PathMapContents { get; set; }
    public DbSet<PlannedResponsePlan> PlannedResponsePlans { get; set; }
    #endregion

    #region Respondent
    public DbSet<Staff> Staffs { get; set; }
    public DbSet<StaffStatistic> StaffStatistics { get; set; }
    public DbSet<RespondentJobSample> RespondentJobSamples { get; set; }
    #endregion
}
