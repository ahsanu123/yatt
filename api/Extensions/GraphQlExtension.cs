using GraphQL;
using YATT.Api.GraphqlSchemas;

namespace YATT.Api.Extensions;

public static class GraphQlExtension
{
    public static IServiceCollection AddGraphQl(this IServiceCollection services)
    {
        services.AddGraphQL(configure =>
        {
            configure.AddAutoSchema<Query>().AddSystemTextJson();
        });
        return services;
    }
}
