// UnitTest1.cs

using Shouldly;
using Xunit;

namespace Api.IntegrationTests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var num = Random.Shared.Next();

        num.ShouldBeGreaterThanOrEqualTo(0);
    }
}
