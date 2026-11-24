// AppDbContext.cs

using Api.Modules.Tenants.Domain;
using Api.Modules.Tenants.Infrastructure;
using Api.Modules.Users.Domain;
using Api.Modules.Users.Infrastructure;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Api.SharedKernel.Persistence;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<ApplicationUser, UserRole, Guid>(options)
{
    public DbSet<Tenant>  Tenants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ApplicationUserConfig());
        modelBuilder.ApplyConfiguration(new UserRoleConfig());
        modelBuilder.ApplyConfiguration(new TenantConfig());
    }

}
