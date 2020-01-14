using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Recademy.Api.Services.Abstraction;
using Recademy.Library.Dto;
using Recademy.Library.Types;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Recademy.Api.Controllers
{
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [Route("{userId}")]
        public IActionResult GetUserInfo([FromQuery] int userId)
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
    }
}