using Dissertation.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dissertation.Common.Services;

public interface IApplicationDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    #region Respondent
    DbSet<Analyst> Analysts { get; set; }
    DbSet<CyberSecuritySpecialist> CyberSecuritySpecialists { get; set; }
    #endregion

    #region File
    DbSet<FileIncident> FileIncidents { get; set; }
    DbSet<FileDetails> FileDetails { get; set; }
    DbSet<VirusTotalReportDetails> VirusTotalReportDetails { get; set; }
    #endregion
}
