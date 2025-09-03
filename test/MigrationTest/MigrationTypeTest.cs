using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;
using YATT.Migrations.Configs;
using YATT.Migrations.Extensions;
using YATT.Migrations.ListMigration;
using YATT.Migrations.Mappers;

using YattIdentityRole = YATT.Libs.Models.IdentityRole;
using YattIdentityUser = YATT.Libs.Models.IdentityUser;

namespace YATT.Tests.Migrations;

public class MigrationTestTest : MigrationBaseTest
{
    public MigrationTestTest(TestFixture testFixture, ITestOutputHelper testOutputHelper)
        : base(testFixture, testOutputHelper) { }

    [Fact]
    public void CheckPropsInsideInheritedClass()
    {
        var props = typeof(YattIdentityRole)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

        foreach (var p in props)
        {
            if (!ModelToMigration.IsExcludedType(p.PropertyType))
                _output.WriteLine($"{p.Name} (Declaring: {p.DeclaringType})");
        }
    }


}
