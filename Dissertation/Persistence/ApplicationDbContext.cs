using System.Reflection;
using Dissertation.Common.Services;
using Dissertation.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

#nullable disable
namespace Dissertation.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IDateTime _dateTime;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IDateTime dateTime) : base(options)
    {
        _dateTime = dateTime;
    }

    #region Respondent
    #endregion

    #region File
    public DbSet<FileIncident> FileIncidents { get; set; }
    public DbSet<FileDetails> FileDetails { get; set; }
    public DbSet<VirusTotalReportDetails> VirusTotalReportDetails { get; set; }
    #endregion

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Created = _dateTime.Now;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModified = _dateTime.Now;
                    break;
            }
        }

        var result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
#nullable enable
