using YATT.Migrations.Attributes;

namespace YATT.Migrations.ListMigration.ModelMigration1;

public class EventType
{

    [PrimaryKey]
    public long Id { get; set; }
}
