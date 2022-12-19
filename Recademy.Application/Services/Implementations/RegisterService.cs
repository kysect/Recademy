using Microsoft.Extensions.Logging;
using Recademy.Application.Services.Abstractions;
using Recademy.Core.Models.Users;
using Recademy.DataAccess;

using System;
using System.Linq;
using System.Security.Claims;

namespace Recademy.Application.Services.Implementations;

public class RegisterService : IRegisterService
{
    private readonly RecademyContext _context;
    private readonly IOauthProviderService _oauthProvider;
    private readonly ILogger<RegisterService> _logger;

    public RegisterService(RecademyContext context, IOauthProviderService oauthProvider, ILogger<RegisterService> logger)
    {
        _context = context;
        _oauthProvider = oauthProvider;
        _logger = logger;
    }

    public void Register(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        if (IsUserRegistered(user))
            return;

        _context.Users.Add(user);
        _context.SaveChanges();

        _logger.LogInformation($"Registered user {user.GithubUsername} with ID {user.Id}");

        var recademyUser = new RecademyUser { UserId = user.Id };

        _context.RecademyUsers.Add(recademyUser);
        _context.SaveChanges();

        _logger.LogInformation($"Registered Recademy user {recademyUser.User.GithubUsername} with ID {recademyUser.UserId}");
    }

    public User GetUserFromClaims(ClaimsPrincipal claims)
    {
        ArgumentNullException.ThrowIfNull(claims);

        if (claims.Identity?.AuthenticationType != "GitHub")
            throw new NotSupportedException("Only GitHub authentication type is supported");

        return _oauthProvider.GetUserFromGithubClaims(claims);

    }

    public bool IsUserRegistered(User user)
    {
        return _context.Users.Any(u => u.GithubUsername == user.GithubUsername);
    }
}