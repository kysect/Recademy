using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Recademy.Application.Services.Abstractions;
using Recademy.Dto.Projects;

namespace Recademy.Api.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/projects")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        public ActionResult<ProjectInfoDto> CreateProject([FromBody] [Required] AddProjectDto addProjectDto)
        {
            //TODO: validate tags - it is must exist in database
            //TODO: validate project url - it is must be project at author github
            return _projectService.AddProject(addProjectDto);
        }

        [HttpGet("{projectId}")]
        public ActionResult<ProjectInfoDto> ReadById([Required] int projectIid)
        {
            return _projectService.GetProjectInfo(projectIid);
        }

        /// <summary>
        ///     Get projects by tag
        /// </summary>
        [HttpGet("tag/{tagName}")]
        public ActionResult<List<ProjectInfoDto>> ReadByTag([Required] string tagName)
        {
            return _projectService.GetProjectsByTag(tagName);
        }
    }
}