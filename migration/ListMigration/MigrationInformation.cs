namespace YATT.Migrations.ListMigration;

public class MigrationInformation
{
    public long Version { get; set; }
    public string Description { get; set; }

    public MigrationInformation(long version, string description)
    {
        Version = version;
        Description = description;
    }
}
