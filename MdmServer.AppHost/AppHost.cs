var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres");
var appDb = postgres.AddDatabase("AppDb");

var api = builder.AddDotnetProject("api", "../Api/Api.csproj")
    .WithHttpHealthCheck("/health")
    .WithReference(appDb)
    .WaitFor(appDb);

var migrations = api.AddEFMigrations("api-migrations",
    "Api.SharedKernel.Persistence.AppDbContext")
    .WithMigrationsProject<Projects.Migrations>()
    .WaitFor(appDb)
    .RunDatabaseUpdateOnStart();

api.WaitForCompletion(migrations);

builder.Build().Run();
