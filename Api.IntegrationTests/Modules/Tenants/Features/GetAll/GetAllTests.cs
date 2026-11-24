// GetAllTests.cs

using System.Net;
using Api.IntegrationTests.Setup;
using Api.Modules.Tenants.Features.GetAll;
using FastEndpoints;
using FastEndpoints.Testing;
using Shouldly;
using Xunit;

namespace Api.IntegrationTests.Modules.Tenants.Features.GetAll;

public class GetAllTests(TestFixture App): TestBase<TestFixture>
{

    [Fact]
    public async Task ShouldGetAll()
    {
        (HttpResponseMessage rsp, GetAllResponse res) = await App.Client.GETAsync<GetAllEndpoint, GetAllResponse>();
        rsp.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}
