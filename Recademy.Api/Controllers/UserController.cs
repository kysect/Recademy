using System.Collections.Generic;
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
        public ActionResult<UserInfoDto> GetUserInfo(int userId)
        {
            if (userId < 0)
                return BadRequest($"Wrong user Id: {userId}");

            try
            {
                UserInfoDto userInfo = _userService.GetUserInfo(userId);
                return Ok(userInfo);
            }
            catch (RecademyException exception)
            {
                return BadRequest(exception);
            }
        }

        /// <summary>
        /// Get user activity ranking
        /// </summary>
        [HttpGet("ranking")]
        public ActionResult<Dictionary<string, int>> GetUsersRanking()
        {
            Dictionary<string, int> ranking = _userService.GetUsersRanking();
            return Ok(ranking);
        }
    }
}