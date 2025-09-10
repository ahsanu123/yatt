using YATT.Libs.Attributes;

namespace YATT.Migrations.ListMigration.TableMigration1;

public class TeamPersonKey
{
    [ForeignKey(typeof(Team), propName: nameof(Team.Id))]
    public long TeamId { get; set; }

    [ForeignKey(typeof(Person), propName: nameof(Person.Id))]
    public long PersonId { get; set; }
}

