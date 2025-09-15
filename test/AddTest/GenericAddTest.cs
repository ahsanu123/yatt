using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
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

    [Fact]
    public async Task GetAllSchemeProvider()
    {
        using var scope = _factory.Services.CreateScope();
        var authSchemeProvider = scope.ServiceProvider.GetService<IAuthenticationSchemeProvider>();

        Assert.NotNull(authSchemeProvider);

        var allScheme = await authSchemeProvider.GetAllSchemesAsync();

        _output.WriteLine(authSchemeProvider.GetType().FullName);
        _output.SerializeObject(
            allScheme.Select(scheme => new
            {
                scheme.Name,
                scheme.DisplayName,
                scheme.HandlerType.FullName,
            })
        );
    }

    [Fact]
    public async Task GetSchemeRequestHandler()
    {
        using var scope = _factory.Services.CreateScope();
        var authSchemeProvider = scope.ServiceProvider.GetService<IAuthenticationSchemeProvider>();

        Assert.NotNull(authSchemeProvider);

        var requestHandlerScheme = await authSchemeProvider.GetRequestHandlerSchemesAsync();

        _output.SerializeObject(
            requestHandlerScheme.Select(scheme => new
            {
                scheme.Name,
                scheme.DisplayName,
                scheme.HandlerType.FullName,
            })
        );
    }

    [Fact]
    public async Task GetAllAuthenticationHandler()
    {
        var client = _factory.CreateClient();
        var authHandlerResponse = await client.GetAsync("/test/get-authentication-handle");
        var content = await authHandlerResponse.Content.ReadAsStringAsync();

        _output.WriteLine(content);
    }
}
