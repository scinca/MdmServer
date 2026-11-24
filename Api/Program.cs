using Api.Modules.Users.Domain;
using Api.SharedKernel.Persistence;
using Api.SharedKernel.UrlFriendlyGuid;
using FastEndpoints;
using FastEndpoints.OpenApi;
using Microsoft.AspNetCore.Identity;
using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddSingleton(TimeProvider.System);

builder.Services.Configure<RouteOptions>(options =>
    options.ConstraintMap.Add("urlfriendlyguid",
        typeof(UrlFriendlyGuidRouteConstraint))
);

builder.Services.ConfigurePersistence(builder.Configuration);

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddDataProtection();
builder.Services.AddIdentityCore<ApplicationUser>()
    .AddRoles<UserRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddFastEndpoints()
    .OpenApiDocument();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference("/",options => options.AddDocuments("v1"));
}

app.MapDefaultEndpoints();

app.UseAuthorization();
app.UseAuthorization();

app.UseFastEndpoints();

app.Run();

public partial class Program;
