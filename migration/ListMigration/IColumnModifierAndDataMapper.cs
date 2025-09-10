namespace YATT.Migrations.ListMigration;

public interface IColumnModifierAndDataMapper
{
    public void MapData(Type oldType, Type newType);
    public void CreateTable();
    public void AlterTable();
    public void Run();
}
