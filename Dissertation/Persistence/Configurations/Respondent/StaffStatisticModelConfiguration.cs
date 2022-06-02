using Dissertation.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dissertation.Persistence.Configurations.Respondent;

public class StaffStatisticModelConfiguration : IEntityTypeConfiguration<StaffStatistic>
{
    public void Configure(EntityTypeBuilder<StaffStatistic> builder)
    {
        builder.ToTable("StaffStatistics");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id);
        builder.Property(x => x.IsRelevance);

        builder.Property(x => x.StatisticsType)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(x => x.Workload)
            .HasConversion<string>()
            .HasDefaultValue(Workload.Neutral)
            .IsRequired();

        builder.HasMany(ss => ss.JobSamples).WithMany(j => j.StaffStatistics);
    }
}
