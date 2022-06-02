using Dissertation.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dissertation.Persistence.Configurations.Plan;

public class PathMapContentModelConfiguration : IEntityTypeConfiguration<PathMapContent>
{
    public void Configure(EntityTypeBuilder<PathMapContent> builder)
    {
        builder.ToTable("PathMapContents");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id);
        builder.Property(x => x.Title).HasMaxLength(256);
        builder.Property(x => x.ResponseToolInfo).HasMaxLength(2560);
        builder.Property(x => x.Source).HasMaxLength(2560);

        builder.Property(x => x.Stage)
            .HasConversion<string>()
            .HasDefaultValue(PathMapStage.Initial)
            .IsRequired();
    }
}
