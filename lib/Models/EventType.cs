using YATT.Migrations.Attributes;

namespace YATT.Libs.Models;

public class EventType
{
    [PrimaryKey]
    public long Id { get; set; }
}
