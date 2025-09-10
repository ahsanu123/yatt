using YATT.Libs.Attributes;

namespace YATT.Migrations.ListMigration.TableMigration1;

public class Client
{
    [PrimaryKey]
    public long Id { get; set; }

    public long Name { get; set; }

    public DateTimeOffset CreatedDate { get; set; }

    public bool Active { get; set; }
}
