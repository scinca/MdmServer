// PostgresContainer.cs

using Api.SharedKernel.Persistence;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Testcontainers.PostgreSql;

namespace Api.IntegrationTests.Setup;

internal static class PostgresContainer
{
    private static readonly SemaphoreSlim s_gate = new(1, 1);
    private static PostgreSqlContainer? s_container;
    private const string Template = "app_template";

    private static async Task EnsureStartedAsync()
    {
        if (s_container is not null) return;
        await s_gate.WaitAsync();
        try
        {
            if (s_container is not null) return;

            var c = new PostgreSqlBuilder("postgres:18.6-alpine").Build();
            await c.StartAsync();

            await using var admin = new NpgsqlConnection(c.GetConnectionString());
            await admin.OpenAsync();
            await Exec(admin, $"CREATE DATABASE {Template}");

            // apply schema ONCE to the template
            string tplCs = new NpgsqlConnectionStringBuilder(c.GetConnectionString()) { Database = Template }
                .ConnectionString;
            await using (var db = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseNpgsql(tplCs)
                             .Options))
                await db.Database.MigrateAsync();

            NpgsqlConnection.ClearAllPools(); // template must have zero open connections
            await Exec(admin, $"ALTER DATABASE {Template} IS_TEMPLATE true");

            s_container = c;
        }
        finally
        {
            s_gate.Release();
        }
    }

    internal static async Task<string> CreateDbAsync()
    {
        await EnsureStartedAsync();
        string name = $"t_{Guid.NewGuid():N}";
        await using var admin = new NpgsqlConnection(s_container!.GetConnectionString());
        await admin.OpenAsync();
        await Exec(admin, $"CREATE DATABASE {name} TEMPLATE {Template}");
        return new NpgsqlConnectionStringBuilder(s_container.GetConnectionString()) { Database = name }.ConnectionString;
    }

    private static async Task Exec(NpgsqlConnection c, string sql)
    {
        await using var cmd = new NpgsqlCommand(sql, c);
        await cmd.ExecuteNonQueryAsync();
    }
}
