using Recademy.Core.Models.Users;

using System.Security.Claims;

namespace Recademy.Application.Services.Abstractions;

public interface IRegisterService
{
    public void Register(User user);
    public User GetUserFromClaims(ClaimsPrincipal claims);
    public bool IsUserRegistered(User user);
}