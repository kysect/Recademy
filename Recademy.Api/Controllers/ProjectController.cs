using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Recademy.Api.Services.Abstraction;
using Recademy.Library.Models;
using Recademy.Library.Types;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Recademy.Api.Controllers
{
    [Route("api/projects")]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        [Route("{projectId}")]
        public IActionResult GetProjectInfo([FromQuery] int projectIid)
        {
            if (projectIid < 0)
                return BadRequest("Wrong project Id");

            try
            {
                ProjectInfo projectInfo = _projectService.GetProjectInfo(projectIid);
                return Ok(projectInfo);
            }
            catch (RecademyException)
            {
                return BadRequest("Wrong project Id");
            }
        }
    }
}