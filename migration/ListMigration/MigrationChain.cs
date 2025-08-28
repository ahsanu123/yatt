namespace YATT.Migrations.ListMigration;

public class MigrationChain
{
    public List<Type> ListType = new List<Type>();

    public MigrationChain AddType(Type type)
    {
        ListType.Add(type);
        return this;
    }
}
