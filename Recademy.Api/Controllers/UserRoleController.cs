using Microsoft.AspNetCore.Mvc;
using Recademy.Application.Services.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Recademy.Api.Attributes;
using Recademy.Dto.Roles;
using System;

namespace Recademy.Api.Controllers;

[Route("api/roles")]
[ApiController]
public class UserRoleController : Controller
{
    private readonly IUserRoleService _userRoleService;

    public UserRoleController(IUserRoleService userRoleService)
    {
        _userRoleService = userRoleService;
    }

    [HttpGet]
    public ActionResult<IReadOnlyCollection<UserRoleDto>> GetAllRoles()
    {
        IReadOnlyCollection<UserRoleDto> roles = _userRoleService.GetAllRoles();

        return Ok(roles);
    }

    [HttpGet("users/{userId}")]
    public ActionResult<UserRoleDto> GetUserRole(int userId)
    {
        try
        {
            UserRoleDto role = _userRoleService.GetUserRole(userId);
            return Ok(role);
        }
        catch (Exception ex)
        {
            // TODO (annchous): log stack
            return BadRequest(ex.Message);
        }
    }

    [RoleRequired]
    [HttpPost("users/{userId}/{roleId}")]
    public async Task<IActionResult> ChangeUserRole(int userId, int roleId)
    {
        await _userRoleService.ChangeUserRole(userId, roleId);
        return Ok();
    }
}