using YATT.Libs.Configs;

namespace YATT.Api.Extensions;

public static class ConfigurationExtension
{
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
