using Microsoft.EntityFrameworkCore;
using Dissertation.Persistence.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dissertation.Persistence.Configurations.File;

public class FileIncidentModelConfiguration : IEntityTypeConfiguration<FileIncident>
{
    public void Configure(EntityTypeBuilder<FileIncident> builder)
    {
        builder.ToTable("FileIncidents");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id);
        builder.Property(x => x.IpAddress).HasMaxLength(30);
        builder.Property(x => x.Domain).HasMaxLength(50);
        builder.Property(x => x.Code).HasMaxLength(512);
        builder.Property(x => x.IsSystemScanClean);
        builder.Property(x => x.IsVirusHashInfoClean);

        builder.Ignore(x => x.Type);

        builder.Property(x => x.FileName).HasMaxLength(256).IsRequired();
        builder.Property(x => x.FolderName).HasMaxLength(256).IsRequired();
        builder.Property(x => x.FullPath).HasMaxLength(256).IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .HasDefaultValue(ScanStatus.NoDefinition)
            .IsRequired();

        builder.HasOne(fi => fi.Details).WithOne(fd => fd.Incident)
            .HasForeignKey<FileDetails>(fd => fd.FileIncidentId);

        builder.Navigation(x => x.Details).AutoInclude();

        builder.HasIndex(x => x.FileName).IsUnique()
            .HasFilter("[FileName] IS NOT NULL"); ;
    }
}
