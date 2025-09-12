using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Yatt.Add.Handlers;

namespace Yatt.Add.AuthenticationProviders;

public static class MicrosoftAuthenticationProvider
{
    public static AuthenticationBuilder AddMicrosoftAuthentication(
        this AuthenticationBuilder builder,
        IConfiguration configuration
    )
    {
        /* Azure AD app model v2 has restrictions that prevent the use of plain HTTP for redirect URLs.
           Therefore, to authenticate through microsoft accounts, try out the sample using the following URL:
           https://localhost:44318/
        */
        // You must first create an app with Microsoft Account and add its ID and Secret to your user-secrets.
        // https://azure.microsoft.com/en-us/documentation/articles/active-directory-v2-app-registration/
        // https://apps.dev.microsoft.com/
        builder.AddMicrosoftAccount(o =>
        {
            o.ClientId = configuration["microsoftaccount:clientid"];
            o.ClientSecret = configuration["microsoftaccount:clientsecret"];
            o.SaveTokens = true;
            o.Scope.Add("offline_access");
            o.Events = new OAuthEvents()
            {
                OnRemoteFailure = FailureHandleCollection.HandleOnRemoteFailure,
            };
        });

        return builder;
    }
}
