using YATT.Libs.Attributes;
using YATT.Libs.Models;

namespace YATT.Migrations.ListMigration.TableMigration1;

public class Rate : IBaseModel
{
    [PrimaryKey]
    public long Id { get; set; }
    public decimal Amount { get; set; }

    public string CurrencyCode { get; set; }

    public DateTimeOffset CreatedDate { get; set; }
}
