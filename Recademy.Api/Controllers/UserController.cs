using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recademy.Api.Services.Abstraction;
using Recademy.Library.Dto;
using Recademy.Library.Types;

namespace Recademy.Api.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get user info
        /// </summary>
        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<UserInfoDto> GetUserInfo(int? userId)
        {
            return userId switch
            {
                null => BadRequest(RecademyException.MissedArgument(nameof(userId))),
                _ when userId < 0 => BadRequest(RecademyException.InvalidArgument(nameof(userId), userId)),
                _ => _userService.GetUserInfo(userId.Value)
            };
        }

        /// <summary>
        /// Get user activity ranking
        /// </summary>
        [HttpGet("ranking")]
        public ActionResult<Dictionary<string, int>> GetUsersRanking()
        {
            return Ok(_userService.GetUsersRanking());
        }
    }
}