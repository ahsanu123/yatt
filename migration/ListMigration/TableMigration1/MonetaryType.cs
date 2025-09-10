using YATT.Libs.Attributes;
using YATT.Libs.Models;

namespace YATT.Migrations.ListMigration.TableMigration1;

public class MonetaryType : IBaseModel
{
    [PrimaryKey]
    public long Id { get; set; }
    public string Name { get; set; }

    public string CurrencyCode { get; set; }

    public string Description { get; set; }
}
