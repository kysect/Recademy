using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Recademy.Api.Services.Abstraction;
using Recademy.Library.Dto;
using Recademy.Library.Types;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        [HttpGet]
        [Route("{userId}")]
        public IActionResult GetUserInfo(int userId)
        {
            if (userId < 0)
                return BadRequest("Wrong user Id");

            try
            {
                UserInfoDto userInfo = _userService.GetUserInfoDto(userId);
                return Ok(userInfo);
            }
            catch (RecademyException)
            {
                return BadRequest("Wrong user Id");
            }
        }

        /// <summary>
        /// Get user activity ranking
        /// </summary>
        [HttpGet]
        [Route("ranking")]
        public IActionResult GetUsersRanking()
        {
            Dictionary<string, int> ranking = _userService.GetRanking();
            return Ok(ranking);
        }


        /// <summary>
        /// Upload project to user
        /// </summary>
        [HttpPost]
        [Route("{userId}")]
        public IActionResult AddUSerProject(int userId, [FromBody] AddProjectDto dto)
        {
            if (dto == null)
                return BadRequest();

            _userService.AddProject(dto);
            return Accepted();
        }
    }
}