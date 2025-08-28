namespace YATT.Migrations.ListMigration;

public interface IColumnModifier
{
    public void AlterTable();
    public void Run();
}
