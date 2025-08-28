using Xunit.Abstractions;

namespace YATT.Tests.Migrations;

public class MigrationBaseTest : IClassFixture<TestFixture>
{
    protected readonly ITestOutputHelper _output;
    protected TestFixture testFixture;

    public MigrationBaseTest(TestFixture testFixture, ITestOutputHelper testOutputHelper)
    {
        _output = testOutputHelper;
        this.testFixture = testFixture;
    }

    public IServiceProvider GetServiceProvider()
    {
        return testFixture.serviceScope.ServiceProvider;
    }

}
