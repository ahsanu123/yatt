using System.Data;
using System.Reflection;
using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using YATT.Migrations.Attributes;
using YATT.Migrations.Extensions;
using YATT.Migrations.ListMigration.ModelMigration1;

namespace YATT.Migrations.Mappers;

public static class ModelToMigration
{
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
            var columnNameWithForeignKey = $"FK_{tableName}_{columnName}_{foreignKeyAttr.Target.DeclaringType!.Name}_{foreignKeyAttr.Target.Name}";

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

        foreach (var prop in modelType.GetProperties())
        {
            var columnName = prop.Name;
            var column = table.WithColumn(prop.Name);

            var type = prop.GetType();
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
            if (foreignKeyAttr != null && !isDontCreateIdentity)
            {
                column.AsInt64().IsNullable(prop);
                continue;
            }

            if (typeof(string) == actualType)
            {
                column.AsString(int.MaxValue).IsNullable(prop);
            }

            if (typeof(bool) == actualType)
            {
                column.AsBoolean().IsNullable(prop);
            }

            if (typeof(DateTime) == actualType)
            {
                column.AsDate().IsNullable(prop);
            }

            if (typeof(DateTimeOffset) == actualType)
            {
                column.AsDateTimeOffset().IsNullable(prop);
            }

            if (typeof(double) == actualType)
            {
                column.AsDouble().IsNullable(prop);
            }

            if (typeof(Int16) == actualType)
            {
                column.AsInt16().IsNullable(prop);
            }

            if (typeof(int) == actualType)
            {
                column.AsInt32().IsNullable(prop);
            }

            if (typeof(Int64) == actualType)
            {
                column.AsInt64().IsNullable(prop);
            }

            if (typeof(float) == actualType)
            {
                column.AsFloat().IsNullable(prop);
            }

            if (typeof(Coordinate) == actualType)
            {
                column.AsString(int.MaxValue).IsNullable(prop);
            }

            if (typeof(byte[]) == actualType)
            {
                column.AsBinary(int.MaxValue).IsNullable(prop);
            }
        }
        return migration;
    }
}
