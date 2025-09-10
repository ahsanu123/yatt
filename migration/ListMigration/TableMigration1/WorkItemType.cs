using YATT.Libs.Attributes;

namespace YATT.Migrations.ListMigration.TableMigration1;

public class WorkItemType
{
    [PrimaryKey]
    public long Id { get; set; }

    public string Name { get; set; } = String.Empty;

    public string Description { get; set; } = String.Empty;

    public DateTimeOffset CreatedDate { get; set; }
}

