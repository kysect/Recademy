using Microsoft.EntityFrameworkCore;
using Recademy.Application.Mappings;
using Recademy.Application.Services.Abstractions;
using Recademy.Core.Models.Projects;
using Recademy.Core.Models.Skills;
using Recademy.DataAccess;
using Recademy.Dto.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recademy.Application.Services.Implementations;

public class ProjectsService : IProjectService
{
    private readonly RecademyContext _context;

    public ProjectsService(RecademyContext context)
    {
        _context = context;
    }

    public ProjectInfoDto GetProjectInfo(int projectId)
    {
        return _context.ProjectInfos
            .Single(project => project.Id == projectId)
            .ToDto();
    }

    public IReadOnlyCollection<ProjectInfoDto> GetProjectsByTag(string tagName)
    {
        return _context.ProjectInfos
            .Where(project => project.Skills.Any(projectSkill => projectSkill.SkillName == tagName))
            .Select(project => project.ToDto())
            .ToList();
    }

    public async Task<ProjectInfoDto> CreateProject(CreateProjectDto createArguments)
    {
        ArgumentNullException.ThrowIfNull(createArguments);

        // TODO: process skills correctly
        var newProject = new ProjectInfo
        {
            AuthorId = createArguments.AuthorId,
            Title = createArguments.Title,
            Description = createArguments.Description,
            GithubLink = createArguments.Link,
            Skills = createArguments.Tags
                .Select(tag => new ProjectSkill { SkillName = tag })
                .ToList()
        };

        _context.ProjectInfos.Add(newProject);
        await _context.SaveChangesAsync();

        // To set User entity value.
        newProject = await _context.ProjectInfos.FindAsync(newProject.Id);

        return newProject.ToDto();
    }

    public async Task<IReadOnlyCollection<ProjectInfoDto>> GetProjectsByUserId(int userId)
    {
        return await _context.ProjectInfos
            .Where(project => project.AuthorId == userId)
            .Select(project => project.ToDto())
            .ToListAsync();
    }
}