using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YATT.Migrations.Configs;

namespace YATT.Migrations.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var yattConnString = configuration.GetConnectionString(ConnectionStringConfig.YattDb);

        services.AddConfig(configuration);
        services.AddSingleton<IConfiguration>(configuration);
        services
            .AddFluentMigratorCore()
            .ConfigureRunner(
                (builder) =>
                    builder
                        .AddSqlServer()
                        .WithGlobalConnectionString(yattConnString)
                        .ScanIn(Assembly.GetExecutingAssembly())
                        .For.All()
            )
            .AddLogging();

        return services;
    }

    public static IServiceCollection AddConfig(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        // csharpier-ignore-start
        services.AddOptions<YattDatabaseConfig>().Bind(configuration.GetSection(nameof(YattDatabaseConfig)));

        // csharpier-ignore-end
        return services;
    }
}
