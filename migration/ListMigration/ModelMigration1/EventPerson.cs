using YATT.Migrations.Attributes;

namespace YATT.Migrations.ListMigration.ModelMigration1;

public class EventPerson
{
    [PrimaryKey]
    public long Id { get; set; }

    [ForeignKey(typeof(Event), propName: nameof(Event.Id))]
    public long EventId { get; set; }

    [ForeignKey(typeof(Person), propName: nameof(Person.Id))]
    public long PersonId { get; set; }
}

