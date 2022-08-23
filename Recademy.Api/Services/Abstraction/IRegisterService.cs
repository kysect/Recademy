using System.Security.Claims;
using Recademy.Core.Models;

namespace Recademy.Api.Services.Abstraction
{
    public interface IRegisterService
    {
        public void Register(User user);
        public User GetUserFromClaims(ClaimsPrincipal claims);
        public bool IsUserRegistered(User user);
    }
}
