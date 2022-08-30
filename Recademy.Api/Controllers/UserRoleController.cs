using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Recademy.Application.Services.Abstractions;
using System.Collections.Generic;

using System.Threading.Tasks;
using System.Linq;
using Recademy.Dto.Roles;

namespace Recademy.Api.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class UserRoleController : Controller
    {
        private readonly IUserRoleService _userRoleService;
        private readonly ILogger<UserAchievementController> _logger;

        public UserRoleController(IUserRoleService userRoleService, ILogger<UserAchievementController> logger)
        {
            _userRoleService = userRoleService;
            _logger = logger;
        }

        [HttpGet("list")]
        public ActionResult<IReadOnlyCollection<UserRoleDto>> GetAllRoles()
        {
            IReadOnlyCollection<UserRoleDto> roles = _userRoleService
                .GetAllRoles()
                .ToList();

            return Ok(roles);
        }

        [HttpGet("users/{userId}")]
        public ActionResult<UserRoleDto> GetUserRole(int userId)
        {
            UserRoleDto role = _userRoleService.GetUserRole(userId);

            return Ok(role);
        }

        [HttpPost("users/{userId}/{roleId}")]
        public async Task<IActionResult> ChangeUserRole(int userId, int roleId)
        {
            await _userRoleService.ChangeUserRole(userId, roleId);
            return Ok();
        }
    }
}
