using Xunit.Abstractions;
using YATT.Libs.Configs;
using YATT.Migrations.Extensions;

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
