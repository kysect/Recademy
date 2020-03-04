using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        ///     Get user info
        /// </summary>
        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<UserInfoDto> ReadUserInfo([Required] int userId)
        {
            return _userService.ReadUserInfo(userId);
        }

        [HttpGet("findById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<UserInfoDto> ReadById([FromQuery][Required] int userId)
        {
            return _userService.FindById(userId);
        }

        [HttpGet("findByUsername")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<UserInfoDto> ReadByUsername([FromQuery][Required] string username)
        {
            return _userService.FindByUsername(username);
        }

        [HttpGet("{userId}/projects")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<ProjectInfoDto>> ReadUserProjects([Required] int userId)
        {
            return _userService.ReadUserProjects(userId);
        }

        [HttpGet("setMentor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<UserInfoDto> UpdateSetMentorRole([FromQuery][Required] int adminId, [FromQuery][Required] int userId)
        {
            return _userService.UpdateUserMentorRole(adminId, userId, UserType.Mentor);
        }

        [HttpGet("removeMentor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<UserInfoDto> UpdateRemoveMentorRole([FromQuery][Required] int adminId, [FromQuery][Required] int userId)
        {
            return _userService.UpdateUserMentorRole(adminId, userId, UserType.CommonUser);
        }
    }
}