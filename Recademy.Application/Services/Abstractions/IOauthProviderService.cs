using Recademy.Core.Models.Users;

using System.Security.Claims;

namespace Recademy.Application.Services.Abstractions
{
    public interface IOauthProviderService
    {
        public User GetUserFromGithubClaims(ClaimsPrincipal claims);
    }
}