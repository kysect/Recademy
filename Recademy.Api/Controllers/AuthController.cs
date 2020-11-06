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
        private readonly IRegisterService _registerService;

        public AuthController(IRegisterService registerService)
        {
            _registerService = registerService;
        }

        [HttpGet("sign-in")]
        public IActionResult SignIn()
        {
            return Challenge(new AuthenticationProperties() { RedirectUri = "/api/auth/register" }, "GitHub");
        }

        [HttpGet("sign-out")]
        public IActionResult SignOut()
        {
            return SignOut(new AuthenticationProperties() { RedirectUri = "/" },
                CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [HttpGet("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Register()
        {
            var user = _registerService.GetUserFromClaims(HttpContext.User);
            
            if (user == null)
                return BadRequest();

            _registerService.Register(user);
            return Ok();
        }
    }
}