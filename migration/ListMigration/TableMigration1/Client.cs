using YATT.Libs.Attributes;
using YATT.Libs.Models;

namespace YATT.Migrations.ListMigration.TableMigration1;

public class Client : IBaseModel
{
    [PrimaryKey]
    public long Id { get; set; }

    public long Name { get; set; }

    public DateTimeOffset CreatedDate { get; set; }

    public bool Active { get; set; }
}
