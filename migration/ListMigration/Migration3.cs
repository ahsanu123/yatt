using FluentMigrator;
using YATT.Migrations.ListMigration.TableMigration1;

namespace YATT.Migrations.ListMigration;

[Migration(MigrationVersionList.Version3, description: MigrationVersionList.DescriptionVersion3)]
public class Migration3 : BaseMigrationRunner
{
    public MigrationChain MigrationChain = new MigrationChain()
        .AddType(typeof(Location))
        .AddType(typeof(Event))
        .AddType(typeof(EventPerson))
        .AddType(typeof(EventType))
        .AddType(typeof(Person))
        .AddType(typeof(Project))
        .AddType(typeof(Report))
        .AddType(typeof(TimeLog))
        .AddType(IdentityStatic.AspnetCoreIdentityModel);

    public override void Up()
    {
        AddForeignKey(MigrationChain);
    }

    public override void Down()
    {
        RemoveForeignKey(MigrationChain);
    }
}
