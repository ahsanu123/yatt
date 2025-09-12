using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using YATT.Add.Constants;
using YATT.Add.Data;

namespace YATT.Add;

public class Program
{
    private static async Task HandleOnRemoteFailure(RemoteFailureContext context)
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "text/html";
        await context.Response.WriteAsync("<html><body>");
        await context.Response.WriteAsync(
            "A remote failure has occurred: <br>"
                + context
                    .Failure.Message.Split(Environment.NewLine)
                    .Select(s => HtmlEncoder.Default.Encode(s) + "<br>")
                    .Aggregate((s1, s2) => s1 + s2)
        );

        if (context.Properties != null)
        {
            await context.Response.WriteAsync("Properties:<br>");
            foreach (var pair in context.Properties.Items)
            {
                await context.Response.WriteAsync(
                    $"-{HtmlEncoder.Default.Encode(pair.Key)}={HtmlEncoder.Default.Encode(pair.Value)}<br>"
                );
            }
        }

        await context.Response.WriteAsync("<a href=\"/\">Home</a>");
        await context.Response.WriteAsync("</body></html>");

        // context.Response.Redirect("/error?FailureMessage=" + UrlEncoder.Default.Encode(context.Failure.Message));

        context.HandleResponse();
    }

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var configuration = builder.Configuration;

        // Add services to the container.
        var connectionString =
            builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "Connection string 'DefaultConnection' not found."
            );

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(connectionString)
        );
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder
            .Services.AddDefaultIdentity<IdentityUser>(option =>
            {
                option.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();

        // # Summary to setup external authentication with google
        // - register / create oauth client in google
        // - add `Authentication.Google` from https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.Google
        // - save client_id and client_secret with _secret manager_ (for development) look at justfile
        // - then Add an Authorized redirect URI as described in docs (https://learn.microsoft.com/en-us/aspnet/core/security/authentication/social/google-logins?view=aspnetcore-9.0)
        // - For local testing, use the default address https://localhost:{PORT}/signin-google, where the {PORT} placeholder is the app's port.
        //
        builder
            .Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie()
            .AddGoogle(option =>
            {
                // csharpier-ignore-start
                option.ClientId = configuration[GoogleAuthenticationConstant.AuthenticationClientId]!;
                option.ClientSecret = configuration[GoogleAuthenticationConstant.AuthenticationClientId]!;
                option.Events = new OAuthEvents()
                {
                    OnRemoteFailure = HandleOnRemoteFailure
                };
                // csharpier-ignore-end
            });

        builder.Services.AddRazorPages();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapStaticAssets();
        app.MapRazorPages().WithStaticAssets();

        app.Run();
    }
}
