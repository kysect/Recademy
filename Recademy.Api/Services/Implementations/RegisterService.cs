using System;
using System.Linq;
using System.Security.Claims;
using Recademy.Api.Services.Abstraction;
using Recademy.Library.Models;

namespace Recademy.Api.Services.Implementations
{
    public class RegisterService : IRegisterService
    {
        private readonly RecademyContext _context;
        private readonly IOauthProviderService _oauthProvider;

        public RegisterService(RecademyContext context, IOauthProviderService oauthProvider)
        {
            _context = context;
            _oauthProvider = oauthProvider;
        }

        public void Register(User user)
        {
            ArgumentNullException.ThrowIfNull(user);

            if (IsUserRegistered(user))
                return;

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User GetUserFromClaims(ClaimsPrincipal claims)
        {
            if (claims.Identity?.AuthenticationType != "GitHub")
                throw new Exception("Only GitHub authentication type is supported");

            return _oauthProvider.GetUserFromGithubClaims(claims);

        }

        public bool IsUserRegistered(User user)
        {
            return _context.Users.Any(u => u.GithubLink == user.GithubLink);
        }
    }
}