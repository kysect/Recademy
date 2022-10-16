using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Recademy.Application.Mappings;
using Recademy.Application.Services.Abstractions;
using Recademy.Dto;
using Recademy.Dto.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using User = Recademy.Core.Models.Users.User;

namespace Recademy.Api.Controllers;

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

            UserInfoDto dto = user.ToDto();

            _logger.LogInformation($"User {dto?.GithubUsername} was successfully authenticated via GitHub");

            string token = GenerateToken(dto);

            HttpContext.Response.Cookies.Append("JwtToken", token);

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
            string username = HttpContext.User
                .FindFirstValue("urn:github:url")?
                .Split('/')
                .LastOrDefault();

            UserInfoDto dto = _userService.FindUserByUsername(username);

            if (dto is null)
                return Unauthorized(username);

            _logger.LogInformation($"Current user is {dto.GithubUsername}");

            string token = GenerateToken(dto);

            HttpContext.Response.Cookies.Append("JwtToken", token);

            return Ok(dto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get current user");
            return Unauthorized($"Failed to get current user: {ex.Message}");
        }
    }

    private string GenerateToken(UserInfoDto userInfo)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Claims = new Dictionary<string, object>()
        };

        tokenDescriptor.Claims.Add("UserType", userInfo.UserType);

        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}