using Xunit.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;

namespace YATT.Tests.Migrations;

public class MigrationRunnerTest : MigrationBaseTest
{
    public MigrationRunnerTest(TestFixture testFixture, ITestOutputHelper testOutputHelper)
        : base(testFixture, testOutputHelper) { }

    [Fact]
    public void MigrateToVersion1()
    {
        var migrationRunner = GetServiceProvider().GetService<IMigrationRunner>();
        var versionLoader = GetServiceProvider().GetService<IVersionLoader>();
        var configuration = GetServiceProvider().GetService<IConfiguration>();

        Assert.NotNull(configuration);
    }
}
