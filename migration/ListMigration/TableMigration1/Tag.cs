using YATT.Libs.Attributes;

namespace YATT.Migrations.ListMigration.TableMigration1;

public class Tag
{
    [PrimaryKey]
    public long Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
}

