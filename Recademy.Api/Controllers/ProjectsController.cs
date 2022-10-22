using Microsoft.AspNetCore.Mvc;
using Recademy.Application.Services.Abstractions;
using Recademy.Dto.Projects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recademy.Api.Controllers;

// Do not change controller route (to lowercase too), either it will not work!
[Route("api/[controller]")]
[ApiController]
public class ProjectsController : Controller
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet("users/{userId}")]
    public async Task<ActionResult<IReadOnlyCollection<ProjectInfoDto>>> GetProjectByUserId(int userId)
    {
        IReadOnlyCollection<ProjectInfoDto> userProjects = await _projectService.GetProjectsByUserId(userId);
        return Ok(userProjects);
    }

    [HttpPost]
    public async Task<ActionResult<ProjectInfoDto>> CreateProject(CreateProjectDto createProjectDto)
    {
        ProjectInfoDto project = await _projectService.CreateProject(createProjectDto);
        return Ok(project);
    }
}