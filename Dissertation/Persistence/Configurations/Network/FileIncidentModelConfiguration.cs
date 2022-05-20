using Dissertation.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dissertation.Persistence.Configurations.Network;

public class NetworkIncidentModelConfiguration : IEntityTypeConfiguration<NetworkIncident>
{
    public void Configure(EntityTypeBuilder<NetworkIncident> builder)
    {
        builder.ToTable("NetworkIncidents");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id);
        builder.Property(x => x.IpAddrees).HasMaxLength(30);
        builder.Property(x => x.Domain).HasMaxLength(50);

        builder.Ignore(x => x.TypeName);
    }
}
