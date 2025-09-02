using YATT.Migrations.Attributes;

namespace YATT.Migrations.ListMigration.ModelMigration1;

public class Report
{
    [PrimaryKey]
    public long Id { get; set; }

    [ForeignKey(typeof(Project), propName: nameof(Project.Id))]
    public long ProjectId { get; set; }
}

