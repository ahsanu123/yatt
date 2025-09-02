using YATT.Migrations.Attributes;

namespace YATT.Migrations.ListMigration.ModelMigration1;

public class Location
{
    [PrimaryKey]
    public long Id { get; set; }

    public string Name { get; set; } = String.Empty;

    public Coordinate? Coordinate { get; set; }
}
