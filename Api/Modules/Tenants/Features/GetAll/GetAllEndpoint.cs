// GetAllEndpoint.cs

using Api.SharedKernel.Persistence;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Api.Modules.Tenants.Features.GetAll;



public class GetAllEndpoint(AppDbContext context) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Head("GetAll");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var content = await context.Tenants.ToListAsync(ct);

        await Send.OkAsync(content, ct);
    }
}
