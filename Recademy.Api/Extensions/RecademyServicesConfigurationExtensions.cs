using Microsoft.Extensions.DependencyInjection;
using Recademy.Application.Services.Abstractions;
using Recademy.Application.Services.Implementations;

namespace Recademy.Api.Extensions;

public static class RecademyServicesConfigurationExtensions
{
    public static void AddRecademyServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IOauthProviderService, OauthProviderService>();
        services.AddScoped<IRegisterService, RegisterService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserAchievementService, UserAchievementService>();
        services.AddScoped<IUserRoleService, UserRoleService>();
        services.AddScoped<IGamificationService, GamificationService>();
        services.AddScoped<IProjectService, ProjectsService>();
        services.AddScoped<IReviewService, ReviewService>();
    }
}