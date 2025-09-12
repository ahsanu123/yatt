using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using YATT.Add.Constants;
using YATT.Add.Data;

namespace YATT.Add;

public class Program
{
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
            .Services.AddDefaultIdentity<IdentityUser>(options =>
                options.SignIn.RequireConfirmedAccount = true
            )
            .AddEntityFrameworkStores<ApplicationDbContext>();

        // # Summary to setup external authentication with google
        // - register / create oauth client in google
        // - add `Authentication.Google` from https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.Google
        // - save client_id and client_secret with _secret manager_ (for development) look at justfile
        // - then Add an Authorized redirect URI as described in docs (https://learn.microsoft.com/en-us/aspnet/core/security/authentication/social/google-logins?view=aspnetcore-9.0)
        // - For local testing, use the default address https://localhost:{PORT}/signin-google, where the {PORT} placeholder is the app's port.
        //
        builder
            .Services.AddAuthentication()
            .AddGoogle(option =>
            {
                // csharpier-ignore-start
                option.ClientId = configuration[GoogleAuthenticationConstant.AuthenticationClientId]!;
                option.ClientSecret = configuration[GoogleAuthenticationConstant.AuthenticationClientId]!;
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

        app.UseAuthorization();

        app.MapStaticAssets();
        app.MapRazorPages().WithStaticAssets();

        app.Run();
    }
}
