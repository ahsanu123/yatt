using YATT.Libs.Attributes;

namespace YATT.Migrations.ListMigration.TableMigration1;

public class Person
{
    [PrimaryKey]
    public long Id { get; set; }
}

