using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YATT.Libs.Configs;
using YATT.Migrations.Configs;

namespace YATT.Migrations.Extensions;

public static class ConfigurationExtension
{
    public static bool IsDatabaseExists(this IServiceProvider serviceProvider, string databaseName)
    {
        var configuration = serviceProvider.GetService<IConfiguration>();

        if (configuration == null)
            throw new NullReferenceException();

        var yattConnString = configuration.GetConnectionString(ConnectionStringConfig.YattDb);

        var masterConnStringModel = yattConnString!.AsConnectionStringModel();
        masterConnStringModel.InitialCatalog = "master";

        using (var conn = new SqlConnection(masterConnStringModel.ToString()))
        {
            conn.Open();
            var databaseCount = conn.ExecuteScalar<int>(
                $"SELECT COUNT(*) FROM sys.databases WHERE name = @dbName",
                new { dbName = databaseName }
            );
            return databaseCount > 0;
        }
    }

    public static void CreateNewDatabaseIfNotExists(this IServiceProvider serviceProvider)
    {
        var configuration = serviceProvider.GetService<IConfiguration>();
        var yattDatabaseConfig = serviceProvider.GetOption<YattDatabaseConfig>();

        if (configuration == null || yattDatabaseConfig == null)
            throw new NullReferenceException();

        var yattConnString = configuration.GetConnectionString(ConnectionStringConfig.YattDb);

        var masterConnStringModel = yattConnString!.AsConnectionStringModel();
        masterConnStringModel.InitialCatalog = "master";

        using (var conn = new SqlConnection(masterConnStringModel.ToString()))
        {
            conn.Open();

            var databaseName = yattDatabaseConfig.Value.DatabaseName;

            if (!serviceProvider.IsDatabaseExists(databaseName))
                conn.Execute($"CREATE DATABASE [{databaseName}]");
        }
    }
}
