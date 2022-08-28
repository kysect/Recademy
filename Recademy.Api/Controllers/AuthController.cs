using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Recademy.Application.Services.Abstractions;
using Recademy.Shared;
using Recademy.Shared.Dtos.Users;
using User = Recademy.Core.Models.User;

namespace Recademy.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IRegisterService _registerService;
        private readonly IUserService _userService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IRegisterService registerService, IUserService userService, ILogger<AuthController> logger)
        {
            _registerService = registerService;
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("sign-in")]
        public IActionResult SignInGithubUser()
        {
            return Challenge(new AuthenticationProperties() { RedirectUri = "/" }, "GitHub");
        }

        [HttpGet("sign-out")]
        public IActionResult SignOutGithubUser()
        {
            return SignOut(new AuthenticationProperties() { RedirectUri = "/" }, CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [HttpGet("github")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserInfoDto> AuthorizeGithubUser()
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

                UserInfoDto dto = new UserInfoDto(user);

                _logger.LogInformation($"User {dto?.GithubUsername} was successfully authenticated via GitHub");

                return Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to register Recademy user");
                return BadRequest($"Failed to register Recademy user: {ex.Message}");
            }
        }

        [HttpGet("user/current")]
        public ActionResult<UserInfoDto> GetCurrentUser()
        {
            try
            {
                String username = HttpContext.User
                    .FindFirstValue("urn:github:url")
                    .Split('/')
                    .LastOrDefault();

                UserInfoDto dto = _userService.FindByUsername(username);

                _logger.LogInformation($"Current user is {dto?.GithubUsername}");

                return Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get current user");
                return Ok($"Failed to get current user: {ex.Message}");
            }
        }
    }
}