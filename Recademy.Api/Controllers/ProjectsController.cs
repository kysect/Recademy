using Microsoft.AspNetCore.Mvc;
using Recademy.Api.Attributes;
using Recademy.Application.Services.Abstractions;
using Recademy.Dto.Projects;
using Recademy.Dto.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recademy.Api.Controllers;

// Do not change controller route (to lowercase too), either it will not work!
[Route("api/[controller]")]
[ApiController]
public class ProjectsController : Controller
{
    private readonly IProjectService _projectService;
    private readonly IAuthService _authService;

    public ProjectsController(IProjectService projectService, IAuthService authService)
    {
        _projectService = projectService;
        _authService = authService;
    }

    [HttpGet("{projectId}")]
    public ActionResult<ProjectInfoDto> GetProjectById(int projectId)
    {
        ProjectInfoDto project = _projectService.GetProjectInfo(projectId);
        return Ok(project);
    }

    [RoleRequired]
    [HttpGet("users/{userId}")]
    public async Task<ActionResult<IReadOnlyCollection<ProjectInfoDto>>> GetProjectByUserId(int userId)
    {
        IReadOnlyCollection<ProjectInfoDto> userProjects = await _projectService.GetProjectsByUserId(userId);
        return Ok(userProjects);
    }

    [HttpGet("user")]
    public async Task<ActionResult<IReadOnlyCollection<ProjectInfoDto>>> GetProjectByCurrentUser()
    {
        UserInfoDto currentUser = _authService.GetCurrentUser(HttpContext.User);
        IReadOnlyCollection<ProjectInfoDto> userProjects = await _projectService.GetProjectsByUserId(currentUser.Id);

        return Ok(userProjects);
    }

    [HttpPost]
    public async Task<ActionResult<ProjectInfoDto>> CreateProject(CreateProjectDto createProjectDto)
    {
        try
        {
            ProjectInfoDto project = await _projectService.CreateProject(createProjectDto);
            return Ok(project);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}