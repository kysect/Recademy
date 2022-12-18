using Microsoft.AspNetCore.Mvc;
using Recademy.Application.Services.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Recademy.Dto.Activity;
using Recademy.Dto.Users;

namespace Recademy.Api.Controllers;

[Route("api/activity")]
[ApiController]
public class UserActivityConroller : Controller
{
    private readonly IUserActivityService _userActivityService;
    private readonly IUserService _userService;

    public UserActivityConroller(IUserActivityService userActivityService, IUserService userService)
    {
        _userActivityService = userActivityService;
        _userService = userService;
    }

    [HttpGet("users")]
    public async Task<ActionResult<IReadOnlyCollection<UserActivityDto>>> GetAllActivity()
    {
        IReadOnlyCollection<UserActivityDto> activity = await _userActivityService.GetAllActivity();
        return Ok(activity);
    }

    [HttpGet("users/{userId}")]
    public async Task<ActionResult<UserActivityDto>> GetUserActivity(int userId)
    {
        RecademyUserDto userDto = _userService.FindById(userId);
        UserActivityDto activity = await _userActivityService.GetUserActivity(userDto.User.GithubUsername);

        return Ok(activity);
    }
}