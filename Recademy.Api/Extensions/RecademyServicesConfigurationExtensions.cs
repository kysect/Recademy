using Microsoft.Extensions.DependencyInjection;
using Recademy.Application.Services.Abstractions;
using Recademy.Application.Services.Implementations;
using Recademy.Application.Tools;
using Recademy.DataAccess.Repositories.Abstractions;
using Recademy.DataAccess.Repositories.Implementations;

namespace Recademy.Api.Extensions;

public static class RecademyServicesConfigurationExtensions
{
    public static void AddRecademyRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
    }

    public static void AddRecademyServices(this IServiceCollection services)
    {
        services.AddScoped<IOauthProviderService, OauthProviderService>();
        services.AddScoped<IRegisterService, RegisterService>();
        services.AddScoped<IGithubApiAccessor, GithubApiAccessor>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IGamificationService, GamificationService>();
        services.AddScoped<IGithubService, GithubService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserAchievementService, UserAchievementService>();
        services.AddScoped<IUserRoleService, UserRoleService>();
    }
}