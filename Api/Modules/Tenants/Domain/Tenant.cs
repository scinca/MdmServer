// Tenant.cs

namespace Api.Modules.Tenants.Domain;

public sealed class Tenant
{
    public Guid Id { get; init; }

    public string Name { get; init; }


    public Tenant(string name)
    {
        Id = Guid.CreateVersion7();
        Name = name;
    }

    #pragma warning disable CS8618
    private Tenant() { } // used by ef
    #pragma warning restore CS8618
}
