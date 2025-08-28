using YATT.Migrations.Attributes;

namespace YATT.Migrations.ListMigration.ModelMigration1;

public class Event
{
    [PrimaryKey]
    public long Id { get; set; }

    [ForeignKey(typeof(EventType), propName: nameof(EventType.Id))]
    public long EventTypeId { get; set; }

    public string Name { get; set; }

}
