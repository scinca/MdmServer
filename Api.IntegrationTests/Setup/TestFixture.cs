// TestFixture.cs

using FastEndpoints.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Time.Testing;

namespace Api.IntegrationTests.Setup;

public class TestFixture : AppFixture<Program>
{
    public FakeTimeProvider TestClock { get; }  = new FakeTimeProvider(Now);

    public static DateTimeOffset Now =>
        new DateTimeOffset(2026, 1,1, 0,0,0, TimeSpan.Zero);

    private string _cs = null!;

    protected override async ValueTask PreSetupAsync()
    {
        _cs = await PostgresContainer.CreateDbAsync();
    }

    protected override void ConfigureApp(IWebHostBuilder a)
    {
        a.UseSetting("ConnectionStrings:AppDb",  _cs);
    }

    protected override void ConfigureServices(IServiceCollection s)
    {
        s.Replace(ServiceDescriptor.Singleton<TimeProvider>(TestClock));
    }
}
