using Api.SharedKernel.UrlFriendlyGuid;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton(TimeProvider.System);

builder.Services.Configure<RouteOptions>(options =>
    options.ConstraintMap.Add("urlfriendlyguid",
        typeof(UrlFriendlyGuidRouteConstraint))
);



var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.Run();
