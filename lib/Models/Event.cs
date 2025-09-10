namespace YATT.Libs.Models;

public class Event : IBaseModel
{
    public long Id { get; set; }

    public long EventTypeId { get; set; }

    public long LocationId { get; set; }

    public string Name { get; set; } = String.Empty;

    public DateTimeOffset Date { get; set; }
}
