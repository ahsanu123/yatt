using Xunit.Abstractions;
using YATT.Migrations.Extensions;
using YATT.Migrations.Configs;
using Microsoft.Extensions.DependencyInjection;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using YATT.Migrations.ListMigration;

namespace YATT.Tests.Migrations;

public class MigrationRunnerTest : MigrationBaseTest
{
    public MigrationRunnerTest(TestFixture testFixture, ITestOutputHelper testOutputHelper)
        : base(testFixture, testOutputHelper) { }

    [Fact]
    public void MakeDatabase()
    {
        var serviceProvider = GetServiceProvider();
        serviceProvider.CreateNewDatabaseIfNotExists();
    }

    [Fact]
    public void MigrateToVersion1()
    {
        var serviceProvider = GetServiceProvider();

        var migrationRunner = serviceProvider.GetService<IMigrationRunner>();
        var versionLoader = serviceProvider.GetService<IVersionLoader>();
        var yattDatabaseConfig = serviceProvider.GetOption<YattDatabaseConfig>();

        Assert.NotNull(migrationRunner);
        Assert.NotNull(versionLoader);
        Assert.NotNull(yattDatabaseConfig);

        migrationRunner.MigrateUp(MigrationVersionList.Version1);
    }
}
