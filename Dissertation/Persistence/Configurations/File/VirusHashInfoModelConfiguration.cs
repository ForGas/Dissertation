using Dissertation.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dissertation.Persistence.Configurations.File;

public class VirusHashInfoModelConfiguration : IEntityTypeConfiguration<VirusHashInfo>
{
    public void Configure(EntityTypeBuilder<VirusHashInfo> builder)
    {
        builder.ToTable("VirusHashInfo");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id);
        builder.Property(x => x.Sha256).HasMaxLength(64).IsRequired();
        builder.Property(x => x.Sha1).HasMaxLength(40);
        builder.Property(x => x.Md5).HasMaxLength(32);
        builder.Property(x => x.IsVirus).IsRequired();
        builder.Property(x => x.Title).HasMaxLength(256);
        builder.Property(x => x.Source).HasMaxLength(256);


        builder.HasIndex(x => x.Sha256)
            .HasFilter("[Sha256] IS NOT NULL");
    }
}
