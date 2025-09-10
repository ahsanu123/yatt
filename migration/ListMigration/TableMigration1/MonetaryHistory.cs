using YATT.Libs.Attributes;

namespace YATT.Migrations.ListMigration.TableMigration1;

public class MonetaryHistory
{
    [PrimaryKey]
    public long Id { get; set; }

    [ForeignKey(typeof(MonetaryType), propName: nameof(MonetaryType.Id))]
    public long Type { get; set; }

    public decimal MoneyAmount { get; set; }

    public DateTimeOffset Date { get; set; }

    public DateTimeOffset UpdatedDate { get; set; }

    public bool Active { get; set; }
}
