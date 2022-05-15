using Dissertation.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dissertation.Persistence.Configurations.File
{
    public class VirusTotalReportDetailsModelConfiguration : IEntityTypeConfiguration<VirusTotalReportDetails>
    {
        public void Configure(EntityTypeBuilder<VirusTotalReportDetails> builder)
        {
            builder.ToTable("VirusTotalReportDetails");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id);
            builder.Property(x => x.ScanId).HasMaxLength(256).IsRequired();
            builder.Property(x => x.Resource).HasMaxLength(256).IsRequired();
            builder.Property(x => x.Permalink).HasMaxLength(256).IsRequired();
            builder.Property(x => x.JsonContent).HasMaxLength(3584);

            builder.HasIndex(x => x.Resource).HasFilter("[Resource] IS NOT NULL"); ;
        }
    }
}
