using YATT.Libs.Attributes;
using YATT.Libs.Models;

namespace YATT.Migrations.ListMigration.TableMigration1;

public class EventPerson : IBaseModel
{
    [PrimaryKey]
    public long Id { get; set; }

    [ForeignKey(typeof(Event), propName: nameof(Event.Id))]
    public long EventId { get; set; }

    [ForeignKey(typeof(Person), propName: nameof(Person.Id))]
    public long PersonId { get; set; }
}
