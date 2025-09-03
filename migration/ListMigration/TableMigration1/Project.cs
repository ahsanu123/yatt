using YATT.Migrations.Attributes;

namespace YATT.Migrations.ListMigration.TableMigration1;

public class Project
{
    [PrimaryKey]
    public long Id { get; set; }

    public string Title { get; set; } = String.Empty;

    public DateTimeOffset StartDate { get; set; }

    public DateTimeOffset? FinishDate { get; set; }
}

