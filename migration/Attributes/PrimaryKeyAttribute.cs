namespace YATT.Migrations.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class PrimaryKeyAttribute : Attribute
{
    public string? PrimaryKeyName = null;

    public PrimaryKeyAttribute() { }

    public PrimaryKeyAttribute(string primaryKey)
    {
        PrimaryKeyName = primaryKey;
    }
}
