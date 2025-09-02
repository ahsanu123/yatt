using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;
using YATT.Migrations.Configs;
using YATT.Migrations.Extensions;
using YATT.Migrations.ListMigration;

namespace YATT.Tests.Migrations;

public class TimeSpanTest : MigrationBaseTest
{
    public TimeSpanTest(TestFixture testFixture, ITestOutputHelper testOutputHelper)
        : base(testFixture, testOutputHelper) { }

    [Fact]
    public void LearnTimeSpan()
    {
        var timeSpan = TimeSpan.FromHours(4);
        var timeSpanString = timeSpan.ToString();

        var parsedTimeSpan = TimeSpan.Parse(timeSpanString);
        var tick = timeSpan.Ticks;

        _output.SerializeObject(parsedTimeSpan);
    }

}
