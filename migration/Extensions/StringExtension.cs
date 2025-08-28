using Microsoft.Data.SqlClient;

namespace YATT.Migrations.Extensions;

public static class StringExtension
{
    public static string BuildConnectionString(this string connectionString, string databaseName)
    {
        var connStringBuilder = new SqlConnectionStringBuilder(connectionString);

        connStringBuilder.InitialCatalog = databaseName;

        return connStringBuilder.ToString();
    }

    public static SqlConnectionStringBuilder AsConnectionStringModel(this string connString) =>
        new SqlConnectionStringBuilder(connString);
}
