using YATT.Libs.Attributes;
using YATT.Libs.Models;

namespace YATT.Migrations.ListMigration.TableMigration1;

public class TimeLog : IBaseModel
{
    [PrimaryKey]
    public long Id { get; set; }

    [ForeignKey(typeof(Project), propName: nameof(Project.Id))]
    public long ProjectId { get; set; }

    public string Summary { get; set; } = String.Empty;

    public string Description { get; set; } = String.Empty;

    public long TimeSpan { get; set; }

    public DateTimeOffset Date { get; set; }
}
