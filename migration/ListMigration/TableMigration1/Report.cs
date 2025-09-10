using YATT.Libs.Attributes;
using YATT.Libs.Models;

namespace YATT.Migrations.ListMigration.TableMigration1;

public class Report : IBaseModel
{
    [PrimaryKey]
    public long Id { get; set; }

    [ForeignKey(typeof(Project), propName: nameof(Project.Id))]
    public long ProjectId { get; set; }
}
