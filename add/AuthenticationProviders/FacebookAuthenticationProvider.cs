using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using YATT.Add.Handlers;

namespace YATT.Add.AuthenticationProviders;

public static class FacebookAuthenticationProvider
{
    public static AuthenticationBuilder AddFacebookAuthentication(
        this AuthenticationBuilder builder,
        IConfiguration configuration
    )
    {
        // You must first create an app with Facebook and add its ID and Secret to your user-secrets.
        // https://developers.facebook.com/apps/
        // https://developers.facebook.com/docs/facebook-login/manually-build-a-login-flow#login
        builder.AddFacebook(o =>
        {
            o.AppId = configuration["facebook:appid"];
            o.AppSecret = configuration["facebook:appsecret"];
            o.Scope.Add("email");
            o.Fields.Add("name");
            o.Fields.Add("email");
            o.SaveTokens = true;
            o.Events = new OAuthEvents()
            {
                OnRemoteFailure = FailureHandleCollection.HandleOnRemoteFailure,
            };
        });

        return builder;
    }
}
