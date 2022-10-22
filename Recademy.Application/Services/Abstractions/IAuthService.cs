using Recademy.Dto.Users;
using System.Security.Claims;

namespace Recademy.Application.Services.Abstractions;

public interface IAuthService
{
    UserInfoDto GetCurrentUser(ClaimsPrincipal userClaims);
}