using Dissertation.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dissertation.Persistence.Configurations.Respondent;

public class StaffModelConfiguration : IEntityTypeConfiguration<Staff>
{
    public void Configure(EntityTypeBuilder<Staff> builder)
    {
        builder.ToTable("Staffs");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id);
        builder.Property(x => x.FirstName).HasMaxLength(256);
        builder.Property(x => x.LastName).HasMaxLength(256);
        builder.Property(x => x.MiddleName).HasMaxLength(256);
        builder.Property(x => x.Email).HasMaxLength(256);

        builder.Property(x => x.Type).HasConversion<string>().IsRequired();

        builder.HasMany(x => x.Statistics).WithOne(st => st.Staff)
            .HasForeignKey(x => x.StaffId);
    }
}
