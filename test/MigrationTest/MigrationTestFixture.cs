using Microsoft.Extensions.DependencyInjection;
using YATT.Migrations.Extensions;

namespace YATT.Tests.Migrations;

public class TestFixture : IDisposable
{
    public ServiceCollection serviceCollection = new ServiceCollection();
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
