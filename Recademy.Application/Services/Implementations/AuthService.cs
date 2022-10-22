using Recademy.Application.Services.Abstractions;
using Recademy.Dto.Users;
using System.Linq;
using System.Security.Claims;

namespace Recademy.Application.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;

    public AuthService(IUserService userService)
    {
        _userService = userService;
    }

    public UserInfoDto GetCurrentUser(ClaimsPrincipal userClaims)
    {
        string username = userClaims
            .FindFirst("urn:github:url")
            ?.Value
            .Split('/')
            .LastOrDefault();

        UserInfoDto dto = _userService.FindUserByUsername(username);

        return dto;
    }
}