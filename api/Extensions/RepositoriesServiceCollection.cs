using YATT.Api.Repositories;
using YATT.Libs.Models;

namespace YATT.Api.Extensions;

public static class RepositoriesServiceCollection
{
    public static IServiceCollection AddRepositoriesCollection(this IServiceCollection services)
    {
        services.AddScoped<AbstractRepository<Event>>();
        return services;
    }
}
