// HostExtension.cs

using Microsoft.EntityFrameworkCore;

namespace Api.SharedKernel.Persistence;

public static class HostExtension
{
    public static IServiceCollection ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("AppDb");

        services.AddDbContextPool<AppDbContext>(options =>
            options.UseNpgsql(connectionString, op =>
            {
                op.MigrationsAssembly("Migrations");
            }));

        return services;
    }
}
