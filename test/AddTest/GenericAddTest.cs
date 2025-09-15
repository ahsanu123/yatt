using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace YATT.Tests.Add;

public class GenericAddTest : AddBaseTest
{
    public GenericAddTest(
        TestWebApplicationFactory<YATT.Add.Program> factory,
        ITestOutputHelper testOutputHelper
    )
        : base(factory, testOutputHelper) { }

    [Fact]
    public Task CheckAuthenticationConfigValue()
    {
        using var scope = _factory.Services.CreateScope();
        var configuration = scope.ServiceProvider.GetService<IConfiguration>();

        Assert.NotNull(configuration);

        var clientId = configuration["YattAuthentication:Google:ClientId"];

        Assert.NotNull(clientId);
        _output.WriteLine(clientId);

        return Task.CompletedTask;
    }
}
