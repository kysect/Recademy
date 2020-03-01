using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Recademy.Api.Services.Abstraction;
using Recademy.Library.Dto;
using Recademy.Library.Types;

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
        [HttpGet("{projectId}")]
        public ActionResult<ProjectInfoDto> GetProjectInfo(int? projectIid)
        {
            return projectIid switch
            {
                null => BadRequest(RecademyException.MissedArgument(nameof(projectIid))),
                _ when projectIid < 0 => BadRequest(RecademyException.InvalidArgument(nameof(projectIid), projectIid)),
                _ => Ok(_projectService.GetProjectInfo(projectIid.Value))
            };
        }

        /// <summary>
        /// Get projects by tag
        /// </summary>
        [HttpGet("tag/{tagName}")]
        public ActionResult<List<ProjectInfoDto>> GetTagProjects(string tagName)
        {
            return tagName switch
            {
                null => BadRequest(RecademyException.MissedArgument(nameof(tagName))),
                _ when string.IsNullOrWhiteSpace(tagName) => BadRequest(RecademyException.InvalidArgument(nameof(tagName), tagName)),
                _ => Ok(_projectService.GetProjectsByTag(tagName))
            };
        }

        /// <summary>
        /// Upload project to user
        /// </summary>
        [HttpPost]
        public ActionResult<ProjectInfoDto> AddUSerProject([FromBody] AddProjectDto addProjectDto)
        {
            return addProjectDto switch
            {
                null => BadRequest(RecademyException.MissedArgument(nameof(addProjectDto))),
                _ => Ok(_projectService.AddProject(addProjectDto))
            };
        }
    }
}