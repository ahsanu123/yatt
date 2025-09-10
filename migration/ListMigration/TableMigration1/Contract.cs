using YATT.Libs.Attributes;
using YATT.Libs.Models;

namespace YATT.Migrations.ListMigration.TableMigration1;

public class Contract : IBaseModel
{
    [PrimaryKey]
    public long Id { get; set; }

    [ForeignKey(typeof(Client), propName: nameof(Client.Id))]
    public long ClientId { get; set; }

    [ForeignKey(typeof(Monetary), propName: nameof(Monetary.Id))]
    public long MonetaryId { get; set; }

    public string Name { get; set; } = String.Empty;

    public DateTimeOffset CreatedDate { get; set; }

    public bool Active { get; set; }
}
