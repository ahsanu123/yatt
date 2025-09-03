using System.Collections;
using System.Data;
using System.Reflection;
using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using YATT.Migrations.Attributes;
using YATT.Migrations.Extensions;
using YATT.Migrations.ListMigration.TableMigration1;

namespace YATT.Migrations.Mappers;

public static class ModelToMigration
{
    private static List<Type> ExludedTypes =>
        new List<Type>
        {
            typeof(IEnumerable<>),
            typeof(ICollection<>),
            typeof(ICollection),
            typeof(IEnumerable),
        };

    public static bool IsExcludedType(Type type)
    {
        type = Nullable.GetUnderlyingType(type) ?? type;
        if (type == typeof(string))
            return false;

        if (type == typeof(byte[]))
            return false;

        return typeof(IEnumerable).IsAssignableFrom(type);
    }

    public static ICreateTableColumnOptionOrWithColumnSyntax IsNullable(
        this ICreateTableColumnOptionOrWithColumnSyntax withColumnSyntax,
        PropertyInfo propInfo
    )
    {
        var isHaveNullableAttribute = propInfo.GetCustomAttribute<NullableAttribute>();
        var isNullable = Nullable.GetUnderlyingType(propInfo.PropertyType);

        if (isHaveNullableAttribute != null || isNullable != null)
            withColumnSyntax.Nullable();

        return withColumnSyntax;
    }

    public static Migration RemoveForeignKeyFromType(
        this Migration migration,
        Type modelType,
        Func<Type, PropertyInfo[]>? propSelector = null
    )
    {
        var tableName = modelType.Name;
        var props = propSelector == null ? modelType.GetProperties() : propSelector(modelType);

        foreach (var prop in props)
        {
            var foreignKeyAttr = prop.GetCustomAttribute<ForeignKeyAttribute>();
            if (foreignKeyAttr == null)
                continue;

            var columnName = prop.Name;
            var columnNameWithForeignKey =
                $"FK_{tableName}_{columnName}_{foreignKeyAttr.Target.DeclaringType!.Name}_{foreignKeyAttr.Target.Name}";

            migration.Delete.ForeignKey(columnNameWithForeignKey).OnTable(tableName);
        }

        return migration;
    }

    public static Migration GenerateForeignKeyFromType(this Migration migration, Type modelType)
    {
        var tableName = modelType.Name;

        foreach (var prop in modelType.GetProperties())
        {
            var columnName = prop.Name;

            var foreignKeyAttr = prop.GetCustomAttribute<ForeignKeyAttribute>();
            if (foreignKeyAttr == null)
                continue;

            var targetForeignTable = foreignKeyAttr.Target.DeclaringType!.Name;
            var targetForeignColumn = foreignKeyAttr.Target.Name;

            migration
                .Create.ForeignKey()
                .FromTable(tableName)
                .ForeignColumn(columnName)
                .ToTable(targetForeignTable)
                .PrimaryColumn(targetForeignColumn)
                .OnDelete(Rule.Cascade)
                .OnDeleteOrUpdate(Rule.Cascade);
        }

        return migration;
    }

    public static Migration GenerateMigrationFromType(
        this Migration migration,
        Type modelType,
        bool addTempToTableNameIfAlreadyExists = false
    )
    {
        var tableName = modelType.Name;
        var isTableNameAlreadyExist = migration.CheckIfTableExists(modelType);

        if (addTempToTableNameIfAlreadyExists && isTableNameAlreadyExist)
            tableName = $"{tableName}_Temp";

        var table = migration.Create.Table(tableName);

        var propBindingFlag =
            BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.Instance;

        foreach (var prop in modelType.GetProperties(propBindingFlag))
        {
            if (IsExcludedType(prop.PropertyType))
                continue;

            var columnName = prop.Name;
            var column = table.WithColumn(prop.Name);

            var propertyType = prop.PropertyType;
            var actualType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;

            var primaryKeyAttr = prop.GetCustomAttribute<PrimaryKeyAttribute>();
            var foreignKeyAttr = prop.GetCustomAttribute<ForeignKeyAttribute>();

            var isDontCreateIdentity = modelType
                .GetCustomAttributes<DontCreateIdentityAttribute>()
                .Any();

            // TODO: Change this long if into better way, with list or map maybe.
            if (primaryKeyAttr != null && !isDontCreateIdentity)
            {
                column.AsInt64().Nullable().Identity().PrimaryKey();
                continue;
            }
            else if (foreignKeyAttr != null && !isDontCreateIdentity)
            {
                column.AsInt64().IsNullable(prop);
                continue;
            }
            else if (typeof(string) == actualType)
            {
                column.AsString(int.MaxValue).Nullable();
            }
            else if (typeof(bool) == actualType)
            {
                column.AsBoolean().IsNullable(prop);
            }
            else if (typeof(DateTime) == actualType)
            {
                column.AsDate().IsNullable(prop);
            }
            else if (typeof(DateTimeOffset) == actualType)
            {
                column.AsDateTimeOffset().IsNullable(prop);
            }
            else if (typeof(double) == actualType)
            {
                column.AsDouble().IsNullable(prop);
            }
            else if (typeof(Int16) == actualType)
            {
                column.AsInt16().IsNullable(prop);
            }
            else if (typeof(int) == actualType)
            {
                column.AsInt32().IsNullable(prop);
            }
            else if (typeof(Int64) == actualType)
            {
                column.AsInt64().IsNullable(prop);
            }
            else if (typeof(float) == actualType)
            {
                column.AsFloat().IsNullable(prop);
            }
            else if (typeof(byte[]) == actualType)
            {
                column.AsBinary(int.MaxValue).IsNullable(prop);
            }
            else if (typeof(Coordinate) == actualType)
            {
                column.AsString(int.MaxValue).IsNullable(prop);
                continue;
            }
            else
                throw new NotSupportedException(
                    $"Table: {tableName}, Unsupported property type {actualType.FullName} on {modelType.Name}.{prop.Name}"
                );
        }
        return migration;
    }
}
