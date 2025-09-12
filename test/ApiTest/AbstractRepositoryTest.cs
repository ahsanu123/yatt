using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;
using YATT.Api.Repositories;
using YATT.Libs.Models;

namespace YATT.Tests.Api;

public class AbstractRepositoryTest : ApiBaseTest
{
    public AbstractRepositoryTest(
        TestWebApplicationFactory<YATT.Api.Program> factory,
        ITestOutputHelper testOutputHelper
    )
        : base(factory, testOutputHelper) { }

    [Fact]
    public Task CheckIfAbstractRepoWork()
    {
        using var scope = _factory.Services.CreateScope();
        var eventRepo = scope.ServiceProvider.GetService<AbstractRepository<Event>>();

        Assert.NotNull(eventRepo);

        return Task.CompletedTask;
    }
}
