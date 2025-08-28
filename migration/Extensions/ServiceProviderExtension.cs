using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace YATT.Migrations.Extensions;

public static class ServiceProviderExtension
{
    public static IOptions<T>? GetOption<T>(this IServiceProvider serviceProvider)
        where T : class
    {
        return serviceProvider.GetRequiredService<IOptions<T>>();
    }
}
