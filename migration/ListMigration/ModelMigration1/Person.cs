using YATT.Migrations.Attributes;

namespace YATT.Migrations.ListMigration.ModelMigration1;

public class Person
{
    [PrimaryKey]
    public long Id { get; set; }
}

