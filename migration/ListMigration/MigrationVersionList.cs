namespace YATT.Migrations.ListMigration;

public static class MigrationVersionList
{
    public const long Version0 = 0;

    public const long Version1 = 202508281040;
    public const long Version2 = 202509021225;
    public const long Version3 = 202509041409;
    public const long Version4 = 202509051509;
    public const long Version5 = 202509061809;

    // csharpier-ignore-start
    public const string DescriptionVersion0 = "Empty Database";
    public const string DescriptionVersion1 = "migrating basic table structure";
    public const string DescriptionVersion2 = "add foreign key to table";
    public const string DescriptionVersion3 = "remove column from table with foreign key";
    public const string DescriptionVersion4 = "add column with foreign key";
    public const string DescriptionVersion5 = "change column / add / delete with old data migration";

    public static List<KeyValuePair<String, MigrationInformation>> ListVersion =>
        new()
        {
            KeyValuePair.Create(nameof(Version0), new MigrationInformation(Version0, DescriptionVersion0)),
            KeyValuePair.Create(nameof(Version1), new MigrationInformation(Version1, DescriptionVersion1)),
            KeyValuePair.Create(nameof(Version2), new MigrationInformation(Version2, DescriptionVersion2)),
            KeyValuePair.Create(nameof(Version3), new MigrationInformation(Version3, DescriptionVersion3)),
            KeyValuePair.Create(nameof(Version4), new MigrationInformation(Version4, DescriptionVersion4)),
            KeyValuePair.Create(nameof(Version5), new MigrationInformation(Version5, DescriptionVersion5)),
        };
    // csharpier-ignore-end
}
