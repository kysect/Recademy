using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using System.Threading.Tasks;
using SameSiteMode = Microsoft.AspNetCore.Http.SameSiteMode;

namespace Recademy.Api.Extensions;

public static class RecademyAuthenticationConfigurationExtensions
{
    public static void AddRecademyAuthentication(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddCors(options => options.AddPolicy("CorsPolicy", policyConfig =>
        {
            policyConfig
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        }));

        services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.LoginPath = "/api/auth/sign-in";
                options.LogoutPath = "/api/auth/sign-out";
            })
            .AddGitHub(options =>
            {
                options.ClientId = configuration["OAuth:GitHub:ClientId"];
                options.ClientSecret = configuration["OAuth:GitHub:ClientSecret"];

                options.CallbackPath = new PathString("/signin-github");
                options.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
                options.TokenEndpoint = "https://github.com/login/oauth/access_token";
                options.UserInformationEndpoint = "https://api.github.com/user";

                options.Scope.Add("read:user");
                options.Scope.Add("user:email");

                options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                options.ClaimActions.MapJsonKey(ClaimTypes.Name, "login");
                options.ClaimActions.MapJsonKey("urn:github:name", "name");
                options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email", ClaimValueTypes.Email);
                options.ClaimActions.MapJsonKey("urn:github:url", "url");

                options.Events.OnCreatingTicket += context =>
                {
                    if (context.AccessToken is { })
                    {
                        context.Identity?.AddClaim(new Claim("access_token", context.AccessToken));
                    }

                    return Task.CompletedTask;
                };

                options.CorrelationCookie.SameSite = SameSiteMode.Lax;
            });
    }
}