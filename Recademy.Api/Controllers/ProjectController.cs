using Microsoft.AspNetCore.Mvc;
using Recademy.Application.Services.Abstractions;
using Recademy.Dto.Projects;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public ActionResult<ProjectInfoDto> CreateProject([FromBody][Required] AddProjectDto addProjectDto)
        {
            //TODO: validate project url - it is must be project at author github
            return _projectService.AddProject(addProjectDto);
        }

        [HttpGet("{projectId}")]
        public ActionResult<ProjectInfoDto> GetById([Required] int projectId)
        {
            return _projectService.GetProjectInfo(projectId);
        }
        
        [HttpGet("tags/{tagName}")]
        public ActionResult<IReadOnlyCollection<ProjectInfoDto>> GetByTag([Required] string tagName)
        {
            return Ok(_projectService.GetProjectsByTag(tagName));
        }
    }
}