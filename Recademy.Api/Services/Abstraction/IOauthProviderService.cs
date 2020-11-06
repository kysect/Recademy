using System.Security.Claims;
using Recademy.Library.Models;

namespace Recademy.Api.Services.Abstraction
{
    public interface IOauthProviderService
    {
        public User GetUserFromGithubClaims(ClaimsPrincipal claims);
    }
}