using FluentMigrator;
using YATT.Migrations.ListMigration.ModelMigration1;

namespace YATT.Migrations.ListMigration;

[Migration(MigrationVersionList.Version1)]
public class Migration1 : Migration
{
    public MigrationChain MigrationChain = new MigrationChain()
        .AddType(typeof(Event))
        .AddType(typeof(EventType))
        .AddType(typeof(Project))
        .AddType(typeof(Report));

    public override void Up()
    {
        this.RunUp(MigrationChain);
    }

    public override void Down()
    {
        this.RunDown(MigrationChain);
    }
}
