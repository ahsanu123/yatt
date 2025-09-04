using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;
using YATT.Migrations.Configs;
using YATT.Migrations.Extensions;
using YATT.Migrations.ListMigration;
using YATT.Migrations.Mappers;

namespace YATT.Tests.Migrations;

public class YattAssemblyTest : MigrationBaseTest
{
    public YattAssemblyTest(TestFixture testFixture, ITestOutputHelper testOutputHelper)
        : base(testFixture, testOutputHelper) { }

    [Fact]
    public void ListEmbeddedResource()
    {
        var assembly = YattAssembly.ExecutingAssembly;
        foreach (var name in assembly.GetManifestResourceNames())
        {
            _output.WriteLine(name);
        }
    }
}
