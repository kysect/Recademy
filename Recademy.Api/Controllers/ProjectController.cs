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
        [HttpGet]
        [Route("{projectId}")]
        public ActionResult<ProjectInfoDto> GetProjectInfo(int projectIid)
        {
            if (projectIid < 0)
                return BadRequest("Wrong project Id");

            try
            {
                ProjectInfoDto projectInfo = _projectService.GetProjectInfo(projectIid);
                return Ok(projectInfo);
            }
            catch (RecademyException exception)
            {
                return BadRequest(exception);
            }
        }

        /// <summary>
        /// Get projects by tag
        /// </summary>
        [HttpGet]
        public ActionResult<List<ProjectInfoDto>> GetTagProjects([FromQuery] string tagName)
        {
            //TODO: add tag validation
            if (string.IsNullOrWhiteSpace(tagName))
                return BadRequest("Wrong tag name");

            List<ProjectInfoDto> projectsByTag = _projectService.GetProjectsByTag(tagName);
            return Ok(projectsByTag);
        }

        /// <summary>
        /// Upload project to user
        /// </summary>
        [HttpPost]
        public ActionResult<ProjectInfoDto> AddUSerProject([FromBody] AddProjectDto dto)
        {
            if (dto == null)
                return BadRequest();

            ProjectInfoDto result = _projectService.AddProject(dto);
            return Accepted(result);
        }
    }
}