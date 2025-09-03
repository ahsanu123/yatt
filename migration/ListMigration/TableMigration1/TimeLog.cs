using YATT.Libs.Attributes;

namespace YATT.Migrations.ListMigration.TableMigration1;

public class TimeLog
{
    [PrimaryKey]
    public long Id { get; set; }

    public string Summary { get; set; } = String.Empty;

    public string Description { get; set; } = String.Empty;

    public long TimeSpan { get; set; }

    public DateTimeOffset Date { get; set; }
}

