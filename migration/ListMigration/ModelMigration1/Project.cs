using YATT.Migrations.Attributes;

namespace YATT.Migrations.ListMigration.ModelMigration1;

public class Project
{
    [PrimaryKey]
    public long Id { get; set; }
}

