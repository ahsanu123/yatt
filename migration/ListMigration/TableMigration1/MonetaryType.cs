using YATT.Libs.Attributes;

namespace YATT.Migrations.ListMigration.TableMigration1;

public class MonetaryType
{
    [PrimaryKey]
    public long Id { get; set; }

    public string Name { get; set; }

    public string CurrencyCode { get; set; }

    public string Description { get; set; }
}
