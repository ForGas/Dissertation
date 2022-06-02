using Dissertation.Persistence.Entities;
using Dissertation.Persistence.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dissertation.Persistence.Configurations.Plan;

public class PlannedResponsePlanModelConfiguration : IEntityTypeConfiguration<PlannedResponsePlan>
{
    public void Configure(EntityTypeBuilder<PlannedResponsePlan> builder)
    {
        builder.ToTable("PlannedResponsePlans");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id);
        builder.Property(x => x.Title).HasMaxLength(256).IsRequired();
        builder.Property(x => x.Performance);

        builder.Property(x => x.Type)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(x => x.IncidentType)
            .HasConversion<string>()
            .HasDefaultValue(IncidentType.NotDefined)
            .IsRequired();

        builder.Property(x => x.Priority)
            .HasConversion<string>()
            .IsRequired();

        builder.HasMany(p => p.PathMaps).WithMany(pm => pm.Plans);

        builder.Navigation(x => x.PathMaps).AutoInclude();
    }
}
