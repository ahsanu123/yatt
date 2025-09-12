using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Yatt.Add.Handlers;

namespace Yatt.Add.AuthenticationProviders;

public static class IdentityServerAuthenticationProvider
{
    public static AuthenticationBuilder AddIdentityServerAuthentication(
        this AuthenticationBuilder builder,
        IConfiguration configuration
    )
    {
        // https://demo.identityserver.io/
        // https://github.com/IdentityServer/IdentityServer4.Demo/blob/master/src/IdentityServer4Demo/Config.cs
        builder.AddOAuth(
            "IdentityServer",
            "Identity Server",
            o =>
            {
                o.ClientId = "interactive.public";
                o.ClientSecret = "secret";
                o.CallbackPath = new PathString("/signin-identityserver");
                o.AuthorizationEndpoint = "https://demo.identityserver.io/connect/authorize";
                o.TokenEndpoint = "https://demo.identityserver.io/connect/token";
                o.UserInformationEndpoint = "https://demo.identityserver.io/connect/userinfo";
                o.ClaimsIssuer = "IdentityServer";
                o.SaveTokens = true;
                o.UsePkce = true;
                // Retrieving user information is unique to each provider.
                o.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "sub");
                o.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
                o.ClaimActions.MapJsonKey(ClaimTypes.Email, "email", ClaimValueTypes.Email);
                o.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "given_name");
                o.ClaimActions.MapJsonKey(ClaimTypes.Surname, "family_name");
                o.ClaimActions.MapJsonKey("email_verified", "email_verified");
                o.ClaimActions.MapJsonKey(ClaimTypes.Uri, "website");
                o.Scope.Add("openid");
                o.Scope.Add("profile");
                o.Scope.Add("email");
                o.Scope.Add("offline_access");
                o.Events = new OAuthEvents
                {
                    OnRemoteFailure = FailureHandleCollection.HandleOnRemoteFailure,
                    OnCreatingTicket = async context =>
                    {
                        // Get the user
                        var request = new HttpRequestMessage(
                            HttpMethod.Get,
                            context.Options.UserInformationEndpoint
                        );
                        request.Headers.Authorization = new AuthenticationHeaderValue(
                            "Bearer",
                            context.AccessToken
                        );
                        request.Headers.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json")
                        );

                        var response = await context.Backchannel.SendAsync(
                            request,
                            context.HttpContext.RequestAborted
                        );
                        response.EnsureSuccessStatusCode();

                        using (
                            var user = JsonDocument.Parse(
                                await response.Content.ReadAsStringAsync()
                            )
                        )
                        {
                            context.RunClaimActions(user.RootElement);
                        }
                    },
                };
            }
        );
        return builder;
    }
}
