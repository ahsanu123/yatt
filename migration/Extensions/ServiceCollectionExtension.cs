using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YATT.Libs.Configs;
using YATT.Libs.Databases;
using YATT.Libs.Identities;
using YATT.Migrations.Configs;
using YattIdentityRole = YATT.Libs.Models.IdentityRole;
using YattIdentityUser = YATT.Libs.Models.IdentityUser;

namespace YATT.Migrations.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        var appSettingStream = YattAssembly.ExecutingAssembly.GetManifestResourceStream(
            EmbeddedContentList.AppSetting
        );

        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonStream(appSettingStream!)
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

        services.AddAspnetCoreIdentity(connectionString: yattConnString!);
        services.AddPromptServices();

        return services;
    }

    public static IServiceCollection AddAspnetCoreIdentity(
        this IServiceCollection services,
        string connectionString
    )
    {
        var connectionFactory = new YattDefaultConnectionFactory(connectionString);

        services
            .AddIdentity<YattIdentityUser, YattIdentityRole>()
            .AddLinqToDBStores(
                connectionFactory,
                typeof(long),
                typeof(YATT.Libs.Models.IdentityUserClaim),
                typeof(YATT.Libs.Models.IdentityUserRole),
                typeof(YATT.Libs.Models.IdentityUserLogin),
                typeof(YATT.Libs.Models.IdentityUserToken),
                typeof(YATT.Libs.Models.IdentityRoleClaim)
            )
            .AddUserManager<YattUserManager>()
            .AddRoleManager<YattRoleManager>()
            .AddDefaultTokenProviders();

        return services;
    }

    public static IServiceCollection AddConfig(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        // csharpier-ignore-start
        services.AddOptions<YattDatabaseConfig>().Bind(configuration.GetSection(nameof(YattDatabaseConfig)));
        services.AddOptions<YattConfig>().Bind(configuration.GetSection(nameof(YattConfig)));

        // csharpier-ignore-end
        return services;
    }
}
