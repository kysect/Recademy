using System.Security.Claims;
using Recademy.Core.Models;

namespace Recademy.Api.Services.Abstraction
{
    public interface IOauthProviderService
    {
        public User GetUserFromGithubClaims(ClaimsPrincipal claims);
    }
}