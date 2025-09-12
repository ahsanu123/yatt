using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Yatt.Add.Handlers;

namespace Yatt.Add.AuthenticationProviders;

public static class GoogleAuthenticationProvider
{
    public static AuthenticationBuilder AddGoogleAuthentication(
        this AuthenticationBuilder builder,
        IConfiguration configuration
    )
    {
        // You must first create an app with Google and add its ID and Secret to your user-secrets.
        // https://console.developers.google.com/project
        // https://developers.google.com/identity/protocols/OAuth2WebServer
        // https://developers.google.com/+/web/people/
        builder.AddGoogle(o =>
        {
            o.ClientId = configuration["YattAuthentication:Google:ClientId"];
            o.ClientSecret = configuration["YattAuthentication:Google:ClientSecret"];
            o.AccessType = "offline";
            o.SaveTokens = true;
            o.Events = new OAuthEvents()
            {
                OnRemoteFailure = FailureHandleCollection.HandleOnRemoteFailure,
            };
            o.ClaimActions.MapJsonSubKey("urn:google:image", "image", "url");
            o.ClaimActions.Remove(ClaimTypes.GivenName);
        });
        return builder;
    }
}
