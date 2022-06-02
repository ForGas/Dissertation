using Dissertation.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dissertation.Persistence.Configurations.Respondent;

public class RespondentJobSampleModelConfiguration : IEntityTypeConfiguration<RespondentJobSample>
{
    public void Configure(EntityTypeBuilder<RespondentJobSample> builder)
    {
        builder.ToTable("RespondentJobSamples");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id);
        builder.Property(x => x.PlanUsageInformation).HasMaxLength(1024).IsRequired();
        builder.Property(x => x.Stage);

        builder.Property(x => x.Stage)
            .HasConversion<string>()
            .HasDefaultValue(Stage.InAcceptance)
            .IsRequired();

        builder.HasOne(r => r.PlannedResponsePlan).WithMany(p => p.RespondentJobSamples)
            .HasForeignKey(x => x.PlannedResponsePlanId);

        builder.HasOne(r => r.FileIncident).WithOne(i => i.JobSample)
            .HasForeignKey<RespondentJobSample>(x => x.IncidentId);

        builder.HasOne(r => r.NetworkIncident).WithOne(i => i.JobSample)
            .HasForeignKey<RespondentJobSample>(x => x.NetworkIncidentId);
    }
}
