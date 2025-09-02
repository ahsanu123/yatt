using FluentMigrator;
using YATT.Migrations.Extensions;
using YATT.Migrations.Mappers;

namespace YATT.Migrations.ListMigration;

public abstract class BaseMigrationRunner : Migration
{
    public void RunUp(MigrationChain migrationChain)
    {
        foreach (var type in migrationChain.ListType)
        {
            this.GenerateMigrationFromType(type);
        }
    }

    public void RunDown(MigrationChain migrationChain)
    {
        migrationChain.ListType.Reverse();
        foreach (var type in migrationChain.ListType)
        {
            this.DeleteTableIfExists(type);
        }
    }

    public void AddForeignKey(MigrationChain migrationChain)
    {
        foreach (var type in migrationChain.ListType)
        {
            this.GenerateForeignKeyFromType(type);
        }
    }

    public void RemoveForeignKey(MigrationChain migrationChain)
    {
        migrationChain.ListType.Reverse();
        foreach (var type in migrationChain.ListType)
        {
            this.RemoveForeignKeyFromType(type);
        }
    }

    public abstract override void Down();
    public abstract override void Up();
}
