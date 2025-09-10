namespace YATT.Api.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddServiceCollections(this IServiceCollection services)
    {
        services.AddRepositoriesCollection();

        return services;
    }
}
