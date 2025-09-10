using YATT.Libs.Attributes;
using YATT.Libs.Models;

namespace YATT.Migrations.ListMigration.TableMigration1;

public class Monetary : IBaseModel
{
    [PrimaryKey]
    public long Id { get; set; }

    [ForeignKey(typeof(Client), propName: nameof(Client.Id))]
    public long ClientId { get; set; }

    [ForeignKey(typeof(MonetaryHistory), propName: nameof(MonetaryHistory.Id))]
    public long MonetaryHistoryId { get; set; }

    public DateTimeOffset CreatedDate { get; set; }

    public bool Active { get; set; }
}
