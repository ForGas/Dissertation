using Dissertation.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dissertation.Persistence.Configurations.File
{
    public class FileDetailsModelConfiguration : IEntityTypeConfiguration<FileDetails>
    {
        public void Configure(EntityTypeBuilder<FileDetails> builder)
        {
            builder.ToTable("FileDetails");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id);
            builder.Property(x => x.Sha256).HasMaxLength(3000);
            builder.Property(x => x.Sha1).HasMaxLength(3000);
            builder.Property(x => x.Md5).HasMaxLength(3000);

            builder.HasOne(fd => fd.Report).WithOne(v => v.FileDetails)
                    .HasForeignKey<VirusTotalReportDetails>(v => v.FileDetailsId);

            builder.HasIndex(x => x.Sha256)
                .HasFilter("[Sha256] IS NOT NULL"); ;

            builder.Navigation(x => x.Report).AutoInclude();
        }
    }
}
