using Dissertation.Common.Services;
using Dissertation.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Dissertation.Persistence;
#nullable disable

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IDateTime _dateTime;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IDateTime dateTime) : base(options)
    {
        _dateTime = dateTime;
    }

    public DbSet<Analyst> Analysts { get; set; }
    public DbSet<CyberSecuritySpecialist> CyberSecuritySpecialists { get; set; }
    public DbSet<FileIncident> Executors { get; set; }
    //public DbSet<Plan> Plans { get; set; }
    //public DbSet<ResponseToolInfo> ResponseToolInfo { get; set; }

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
