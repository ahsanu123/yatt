using YATT.Libs.Attributes;

namespace YATT.Migrations.ListMigration.TableMigration1;

public class EventType
{
    [PrimaryKey]
    public long Id { get; set; }

    public string DisplayName { get; set; } = String.Empty;
}
