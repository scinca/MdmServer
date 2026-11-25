// ApplicationUserConfig.cs

using Api.Modules.Users.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Modules.Users.Infrastructure;

public class ApplicationUserConfig: IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> entity)
    {
        // configurationBuilder OnConfiguring if more models will have initials.
        entity.Property(e => e.Initials)
            .HasConversion<InitialsEfConverter>()
            .HasMaxLength(2)
            .IsFixedLength();
    }
}
