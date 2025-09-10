using YATT.Libs.Attributes;

namespace YATT.Migrations.ListMigration.TableMigration1;

public class Rate
{
    [PrimaryKey]
    public long Id { get; set; }

    public decimal Amount { get; set; }

    public string CurrencyCode { get; set; }

    public DateTimeOffset CreatedDate { get; set; }
}

