using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Recademy.Api.Services.Abstraction;
using Recademy.Library.Dto;
using Recademy.Library.Types;

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

        /// <summary>
        /// Get project info
        /// </summary>
        [HttpGet("{projectId}")]
        public ActionResult<ProjectInfoDto> GetProjectInfo([Required]int projectIid)
        {
            return projectIid switch
            {
                _ when projectIid < 0 => BadRequest(RecademyException.InvalidArgument(nameof(projectIid), projectIid)),
                _ => Ok(_projectService.GetProjectInfo(projectIid))
            };
        }

        /// <summary>
        /// Get projects by tag
        /// </summary>
        [HttpGet("tag/{tagName}")]
        public ActionResult<List<ProjectInfoDto>> GetTagProjects([Required]string tagName)
        {
            return _projectService.GetProjectsByTag(tagName);
        }

        /// <summary>
        /// Upload project to user
        /// </summary>
        [HttpPost]
        public ActionResult<ProjectInfoDto> AddUserProject([FromBody][Required] AddProjectDto addProjectDto)
        {
            //TODO: validate tags - it is must exist in database
            //TODO: validate project url - it is must be project at author github
            return _projectService.AddProject(addProjectDto);
        }
    }
}