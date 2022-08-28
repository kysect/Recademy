using System.Security.Claims;
using Recademy.Core.Models;

namespace Recademy.Application.Services.Abstractions
{
    public interface IOauthProviderService
    {
        public User GetUserFromGithubClaims(ClaimsPrincipal claims);
    }
}