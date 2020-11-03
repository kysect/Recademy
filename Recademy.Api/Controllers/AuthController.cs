using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recademy.Api.Services.Abstraction;

namespace Recademy.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IOauthProviderService _oauthProvider;
        private readonly IRegisterService _registerService;

        public AuthController(IOauthProviderService oauthProvider, IRegisterService registerService)
        {
            _oauthProvider = oauthProvider;
            _registerService = registerService;
        }

        [HttpGet("signIn")]
        public IActionResult SignIn(string provider)
        {
            return Challenge(new AuthenticationProperties() { RedirectUri = "/api/auth/register" }, provider);
        }

        [HttpGet("signOut")]
        public IActionResult SignOut()
        {
            return SignOut(new AuthenticationProperties() { RedirectUri = "/" },
                CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [Authorize]
        [HttpGet("register")]
        public IActionResult Register()
        {
            var user = _registerService.GetUserFromClaims(HttpContext.User);
            
            if (user == null)
                return BadRequest();

            _registerService.Register(user);
            return Redirect("/");
        }
    }
}