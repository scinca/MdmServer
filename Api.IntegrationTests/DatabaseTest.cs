// DatabaseTest.cs

using Api.IntegrationTests.Setup;
using Api.SharedKernel.Persistence;
using FastEndpoints.Testing;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace Api.IntegrationTests;

public class DatabaseTest(TestFixture App) : TestBase<TestFixture>
{
    [Fact]
    public async Task Database_Is_Reachable()
    {
        await using var scope = App.Services.CreateAsyncScope();
        var db =  scope.ServiceProvider.GetRequiredService<AppDbContext>();

        bool canConnect = await db.Database.CanConnectAsync(TestContext.Current.CancellationToken);
        canConnect.ShouldBeTrue();
    }
}
