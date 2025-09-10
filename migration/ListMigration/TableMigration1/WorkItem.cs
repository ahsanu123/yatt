using YATT.Libs.Attributes;

namespace YATT.Migrations.ListMigration.TableMigration1;

public class WorkItem
{
    [PrimaryKey]
    public long Id { get; set; }

    [ForeignKey(typeof(WorkItemType), propName: nameof(WorkItemType.Id))]
    public long WorkItemTypeId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
}

