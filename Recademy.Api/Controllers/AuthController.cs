using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recademy.Api.Services.Abstraction;
using Recademy.Library.Models;

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
        public IActionResult SignInGithubUser()
        {
            return Challenge(new AuthenticationProperties() { RedirectUri = "/api/auth/github" }, "GitHub");
        }

        [HttpGet("sign-out")]
        public IActionResult SignOutGithubUser()
        {
            return SignOut(new AuthenticationProperties() { RedirectUri = "/" }, CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [HttpGet("/github")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AuthorizeGithubUser()
        {
            ClaimsPrincipal userClaims = HttpContext.User;
            Claim accessToken = userClaims.Claims.FirstOrDefault(claim => claim.Type == "access_token");

            if (accessToken is null)
                return BadRequest("Failed to get access token for user");

            GhUtil.Token = accessToken.Value;

            try
            {
                User user = _registerService.GetUserFromClaims(HttpContext.User);

                _registerService.Register(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest($"Failed to register Recademy user: {ex.Message}");
            }

            return Redirect("/");
        }
    }
}