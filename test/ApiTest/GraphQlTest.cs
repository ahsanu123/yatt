using Xunit.Abstractions;

namespace YATT.Tests.Api;

public class GraphQlTest : ApiBaseTest
{
    public GraphQlTest(
        TestWebApplicationFactory<YATT.Api.Program> factory,
        ITestOutputHelper testOutputHelper
    )
        : base(factory, testOutputHelper) { }

    [Fact]
    public async Task GetGraphqlData()
    {
        var client = _factory.CreateClient();

        var result = await client.GetAsync("/graphql?query={hero}");
        var data = await result.Content.ReadAsStringAsync();

        Assert.NotEmpty(data);

        _output.WriteLine(data);
    }
}
