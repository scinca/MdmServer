// GetAllEndpoint.cs

using Api.Modules.Tenants.Domain;
using Api.SharedKernel.Persistence;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Api.Modules.Tenants.Features.GetAll;

public class GetAllResponse(ICollection<Tenant> tenants)
{
    public ICollection<Tenant> Tenants { get; set; } = tenants;
}


public class GetAllEndpoint(AppDbContext context) : EndpointWithoutRequest<GetAllResponse>
{
    public override void Configure()
    {
        Get("GetAll");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var content = await context.Tenants.ToListAsync(ct);

        await Send.OkAsync(new GetAllResponse(content), ct);
    }
}
