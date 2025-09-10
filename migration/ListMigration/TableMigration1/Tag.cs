using YATT.Libs.Attributes;
using YATT.Libs.Models;

namespace YATT.Migrations.ListMigration.TableMigration1;

public class Tag : IBaseModel
{
    [PrimaryKey]
    public long Id { get; set; }
    public string Name { get; set; }

    public string Description { get; set; }
}
