using YATT.Libs.Attributes;
using YATT.Libs.Models;

namespace YATT.Migrations.ListMigration.TableMigration1;

public class Person : IBaseModel
{
    [PrimaryKey]
    public long Id { get; set; }

    [ForeignKey(typeof(Rate), propName: nameof(Rate.Id))]
    public long RateId { get; set; }

    public string Username { get; set; }

    public string FullName { get; set; }
}
