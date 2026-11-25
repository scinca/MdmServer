// TenantConfig.cs

using Api.Modules.Tenants.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Modules.Tenants.Infrastructure;

public class TenantConfig : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> entity)
    {
        entity.Property(e => e.Id).ValueGeneratedNever();

        entity.Property(e => e.Name)
            .HasMaxLength(256);
    }
}
