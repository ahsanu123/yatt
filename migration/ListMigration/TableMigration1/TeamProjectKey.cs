using YATT.Libs.Attributes;

namespace YATT.Migrations.ListMigration.TableMigration1;

public class TeamProjectKey
{
    [ForeignKey(typeof(Team), propName: nameof(Team.Id))]
    public long TeamId { get; set; }

    [ForeignKey(typeof(Project), propName: nameof(Project.Id))]
    public long ProjectId { get; set; }
}
