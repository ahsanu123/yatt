using YATT.Libs.Attributes;
using YATT.Libs.Models;

namespace YATT.Migrations.ListMigration.TableMigration1;

public class EventType : IBaseModel
{
    [PrimaryKey]
    public long Id { get; set; }
    public string DisplayName { get; set; } = String.Empty;
}
