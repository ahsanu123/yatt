using System.Reflection;

namespace YATT.Libs.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class ForeignKeyAttribute : Attribute
{
    public PropertyInfo Target;

    public ForeignKeyAttribute(Type target, string propName)
    {
        var targetProp = target.GetProperty(propName);
        if (targetProp == null)
            throw new NullReferenceException();

        Target = targetProp;
    }
}
