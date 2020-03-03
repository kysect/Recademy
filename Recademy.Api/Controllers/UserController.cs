using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recademy.Api.Services.Abstraction;
using Recademy.Library.Dto;

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
        public ActionResult<UserInfoDto> FindById([FromQuery][Required] int userId)
        {
            return _userService.FindById(userId);
        }

        [HttpGet("findByUsername")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<UserInfoDto> FindByUsername([FromQuery][Required] string username)
        {
            return _userService.FindByUsername(username);
        }

        [HttpGet("{userId}/projects")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<ProjectInfoDto>> ReadUserProjects([Required] int userId)
        {
            return _userService.ReadUserProjects(userId);
        }

        /// <summary>
        ///     Get user activity ranking
        /// </summary>
        [HttpGet("ranking")]
        public ActionResult<Dictionary<string, int>> GetUsersRanking()
        {
            return _userService.GetUsersRanking();
        }
    }
}