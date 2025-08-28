using Xunit.Abstractions;
using YATT.Migrations.Extensions;
using YATT.Migrations.Configs;

namespace YATT.Tests.Migrations;

public class ConfigurationTest : MigrationBaseTest
{
    public ConfigurationTest(TestFixture testFixture, ITestOutputHelper testOutputHelper)
        : base(testFixture, testOutputHelper) { }

    [Fact]
    public void GetYattConfig()
    {
        var yattConfig = GetServiceProvider().GetOption<YattDatabaseConfig>();

        Assert.NotNull(yattConfig);

        _output.SerializeObject(yattConfig.Value);
    }
}
