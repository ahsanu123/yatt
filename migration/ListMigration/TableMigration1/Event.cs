using YATT.Libs.Attributes;
using YATT.Libs.Models;

namespace YATT.Migrations.ListMigration.TableMigration1;

public class Event : IBaseModel
{
    [PrimaryKey]
    public long Id { get; set; }

    [ForeignKey(typeof(EventType), propName: nameof(EventType.Id))]
    public long EventTypeId { get; set; }

    [ForeignKey(typeof(Location), propName: nameof(Location.Id))]
    public long LocationId { get; set; }

    public string Name { get; set; } = String.Empty;

    public DateTimeOffset Date { get; set; }
}
