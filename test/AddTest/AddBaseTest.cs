using Xunit.Abstractions;

namespace YATT.Tests.Add;

public class AddBaseTest : IClassFixture<TestWebApplicationFactory<YATT.Add.Program>>
{
    protected readonly ITestOutputHelper _output;
    protected readonly TestWebApplicationFactory<YATT.Add.Program> _factory;
    protected readonly HttpClient _httpClient;

    public AddBaseTest(
        TestWebApplicationFactory<YATT.Add.Program> factory,
        ITestOutputHelper testOutputHelper
    )
    {
        _factory = factory;
        _httpClient = factory.CreateClient();
        _output = testOutputHelper;
    }
}
