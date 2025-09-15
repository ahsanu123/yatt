using YATT.Api.Extensions;

namespace YATT.Api;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddOpenApi();
        builder.Services.AddControllers();
        builder.Services.AddGraphQl();

        builder.Services.AddServiceCollections();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();
        app.UseWebSockets();
        app.UseGraphQL();

        app.UseGraphQLGraphiQL(
            "/",
            new GraphQL.Server.Ui.GraphiQL.GraphiQLOptions
            {
                GraphQLEndPoint = "/graphql",
                SubscriptionsEndPoint = "/graphql",
            }
        );

        app.MapControllers();

        app.Run();
    }
}
