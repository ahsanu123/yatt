using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace YATT.Add.Extensions;

public static class MapTestExtension
{
    public static void AddMapTestCollection(this IApplicationBuilder app)
    {
        app.Map(
            "/test/get-authentication-handle",
            (testApp) =>
            {
                testApp.Run(async context =>
                {
                    var handlerProvider =
                        context.RequestServices.GetService<IAuthenticationHandlerProvider>();

                    if (handlerProvider == null)
                        await context.Response.WriteAsJsonAsync(new { FullName = "Null" });

                    var handler = await handlerProvider.GetHandlerAsync(
                        context,
                        CookieAuthenticationDefaults.AuthenticationScheme
                    );

                    await context.Response.WriteAsJsonAsync(new { handler.GetType().FullName });
                });
            }
        );
    }
}
