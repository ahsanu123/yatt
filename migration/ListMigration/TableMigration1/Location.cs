using YATT.Migrations.Attributes;

namespace YATT.Migrations.ListMigration.TableMigration1;

public class Location
{
    [PrimaryKey]
    public long Id { get; set; }

    public string Name { get; set; } = String.Empty;

    public Coordinate? Coordinate { get; set; }
}
