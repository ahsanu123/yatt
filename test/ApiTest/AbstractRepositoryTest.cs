using Microsoft.Extensions.DependencyInjection;
using YATT.Api.Repositories;
using YATT.Libs.Models;

namespace YATT.Tests.Api;

public class AbstractRepositoryTest : ApiBaseTest
{
    public AbstractRepositoryTest(TestWebApplicationFactory<Program> factory)
        : base(factory) { }

    [Fact]
    public Task CheckIfAbstractRepoWork()
    {
        using var scope = _factory.Services.CreateScope();
        var eventRepo = scope.ServiceProvider.GetService<AbstractRepository<Event>>();

        Assert.NotNull(eventRepo);

        return Task.CompletedTask;
    }
}
