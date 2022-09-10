using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recademy.Application.Services.Abstractions;
using Recademy.Core.Types;
using Recademy.Dto.Projects;
using Recademy.Dto.Users;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Recademy.Api.Controllers;

[Produces("application/json")]
[Consumes("application/json")]
[Route("api/users")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<RecademyUserDto> GetById([Required] int userId)
    {
        return _userService.GetById(userId);
    }

    [HttpGet("{username}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<RecademyUserDto> GetByUsername([Required] string username)
    {
        return _userService.FindRecademyUserByUsername(username);
    }

    [HttpGet("{userId}/projects")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyCollection<ProjectInfoDto>> GetUserProjects([Required] int userId)
    {
        return Ok(_userService.GetProjectsByUserId(userId));
    }

    // TODO: refactor not to pass admin id
    [HttpPost("{adminId}/users/{userId}/role/mentor")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<UserInfoDto> SetMentorRole([Required] int adminId, [Required] int userId)
    {
        return _userService.UpdateUserRole(adminId, userId, UserType.Mentor);
    }

    [HttpDelete("{adminId}/users/{userId}/role/mentor")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<UserInfoDto> RemoveMentorRole([Required] int adminId, [Required] int userId)
    {
        return _userService.UpdateUserRole(adminId, userId, UserType.CommonUser);
    }
}