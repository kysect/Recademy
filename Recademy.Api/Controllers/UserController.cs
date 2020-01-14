using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Recademy.BlazorWeb.Services.Abstraction;

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
            throw new NotImplementedException();
        }
    }
}