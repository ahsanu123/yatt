using FluentMigrator;
using YATT.Migrations.Extensions;
using YATT.Migrations.Mappers;

namespace YATT.Migrations.ListMigration;

public static class MigrationUtility
{
    public static void RunUp(this Migration migration, MigrationChain migrationChain)
    {
        foreach (var type in migrationChain.ListType)
        {
            migration.GenerateMigrationFromType(type);
        }
    }

    public static void RunDown(this Migration migration, MigrationChain migrationChain)
    {
        migrationChain.ListType.Reverse();
        foreach (var type in migrationChain.ListType)
        {
            migration.DeleteTableIfExists(type);
        }
    }
}
