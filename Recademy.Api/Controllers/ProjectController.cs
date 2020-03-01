using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Recademy.Api.Services.Abstraction;
using Recademy.Library.Dto;
using Recademy.Library.Models;
using Recademy.Library.Types;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Recademy.Api.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/projects")]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }


        /// <summary>
        /// Get project info
        /// </summary>
        [HttpGet]
        [Route("{projectId}")]
        public IActionResult GetProjectInfo(int projectIid)
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

        /// <summary>
        /// Get projects by tag
        /// </summary>
        [HttpGet]
        public IActionResult GetTagProjects([FromQuery] string tagName)
        {
            if (string.IsNullOrWhiteSpace(tagName))
                return BadRequest("Wrong tag name");

            List<ProjectDto> tagProfile = _projectService.GetProjectsByTag(tagName);

            return Ok(tagProfile);
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

            _projectService.AddProject(dto);
            return Accepted();
        }
    }
}