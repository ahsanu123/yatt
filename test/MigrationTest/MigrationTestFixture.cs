using YATT.Migrations.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace YATT.Tests.Migrations;

public class TestFixture : IDisposable
{
    protected ServiceCollection serviceCollection = new ServiceCollection();
    public AsyncServiceScope serviceScope;

    public TestFixture()
    {
        serviceCollection.RegisterServices();
        var serviceProvider = serviceCollection.BuildServiceProvider();

        serviceScope = serviceProvider.CreateAsyncScope();
    }

    public void Dispose()
    {
        serviceScope.Dispose();
    }
}
