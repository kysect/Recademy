using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.IIS.Core;
using Recademy.Api.Services.Abstraction;
using Recademy.Library.Models;
using Recademy.Library.Types;

namespace Recademy.Api.Services.Implementations
{
    public class OauthProviderService : IOauthProviderService
    {
        public User GetUserFromGithubClaims(ClaimsPrincipal claims)
        {
            if (!IsEnoughUserInfo(claims))
                return null;

            var githubLink = claims.FindFirstValue("urn:github:url");
            var githubLogin = githubLink.Split('/').LastOrDefault();

            var user = new User
            {
                Name = claims.FindFirst(c => c.Type == ClaimTypes.Name)?.Value,
                GithubLink = githubLogin,
                UserType = UserType.CommonUser
            };

            return user;
        }

        private bool IsEnoughUserInfo(ClaimsPrincipal claims)
        {
            return claims.HasClaim(c => c.Type == ClaimTypes.Name) && claims.Claims.Any(c => c.Type == "urn:github:url");
        }
    }
}