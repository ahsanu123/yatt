using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;
using YATT.Migrations.Configs;
using YATT.Migrations.Extensions;
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
    public void MigrateDownToVersion0()
    {
        var serviceProvider = GetServiceProvider();

        var migrationRunner = serviceProvider.GetService<IMigrationRunner>();
        var versionLoader = serviceProvider.GetService<IVersionLoader>();
        var yattDatabaseConfig = serviceProvider.GetOption<YattDatabaseConfig>();

        Assert.NotNull(migrationRunner);
        Assert.NotNull(versionLoader);
        Assert.NotNull(yattDatabaseConfig);

        migrationRunner.MigrateDown(MigrationVersionList.version0);
    }

    [Fact]
    public void MigrateDownToVersion1()
    {
        var serviceProvider = GetServiceProvider();

        var migrationRunner = serviceProvider.GetService<IMigrationRunner>();
        var versionLoader = serviceProvider.GetService<IVersionLoader>();
        var yattDatabaseConfig = serviceProvider.GetOption<YattDatabaseConfig>();

        Assert.NotNull(migrationRunner);
        Assert.NotNull(versionLoader);
        Assert.NotNull(yattDatabaseConfig);

        migrationRunner.MigrateDown(MigrationVersionList.Version1);
    }

    [Fact]
    public void MigrateUpToVersion1()
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

    [Fact]
    public void MigrateUpToVersion2()
    {
        var serviceProvider = GetServiceProvider();

        var migrationRunner = serviceProvider.GetService<IMigrationRunner>();
        var versionLoader = serviceProvider.GetService<IVersionLoader>();
        var yattDatabaseConfig = serviceProvider.GetOption<YattDatabaseConfig>();

        Assert.NotNull(migrationRunner);
        Assert.NotNull(versionLoader);
        Assert.NotNull(yattDatabaseConfig);

        migrationRunner.MigrateUp(MigrationVersionList.Version2);
    }
}
