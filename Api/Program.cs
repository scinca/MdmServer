using Api.Modules.Users.Domain;
using Api.SharedKernel.Persistence;
using Api.SharedKernel.UrlFriendlyGuid;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton(TimeProvider.System);

builder.Services.Configure<RouteOptions>(options =>
    options.ConstraintMap.Add("urlfriendlyguid",
        typeof(UrlFriendlyGuidRouteConstraint))
);

builder.Services.ConfigurePersistence(builder.Configuration);
builder.Services.AddIdentityCore<ApplicationUser>()
    .AddRoles<UserRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddFastEndpoints();



var app = builder.Build();

app.UseAuthorization();
app.UseAuthorization();

app.UseFastEndpoints();

app.MapGet("/", () => "Hello World!");

app.Run();
