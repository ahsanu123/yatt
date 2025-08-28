using FluentMigrator;

namespace YATT.Migrations.Extensions;

public static class FluentMigratorExtension
{
    public static bool CheckIfTableExists(this Migration migration, Type type)
    {
        return migration.Schema.Table(type.Name).Exists();
    }

    public static Migration DeleteTableIfExists(this Migration migration, Type type)
    {
        migration.Delete.Table($"{type.Name}_Temp").IfExists();
        migration.Delete.Table(type.Name).IfExists();

        return migration;
    }
}
