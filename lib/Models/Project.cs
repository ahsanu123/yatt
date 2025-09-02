using YATT.Migrations.Attributes;

namespace YATT.Libs.Models;

public class Project
{
    [PrimaryKey]
    public long Id { get; set; }
}

