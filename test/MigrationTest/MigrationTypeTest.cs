using System.Globalization;
using System.Reflection;
using Xunit.Abstractions;
using YATT.Migrations.Mappers;

using YattIdentityRole = YATT.Libs.Models.IdentityRole;

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

    [Fact]
    public void CheckDecimalType()
    {
        var type = typeof(decimal);

        _output.WriteLine(type.Name);
    }

    [Fact]
    public void ListCulture()
    {
        foreach (var culture in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
        {
            var region = new RegionInfo(culture.Name);

            _output.WriteLine($"{region.DisplayName} -> {region.ISOCurrencySymbol}");

        }
    }

}
