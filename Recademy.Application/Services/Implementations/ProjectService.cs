using Recademy.Application.Mappings;
using Recademy.Application.Services.Abstractions;
using Recademy.Core.Models.Projects;
using Recademy.Core.Models.Skills;
using Recademy.DataAccess;
using Recademy.Dto.Projects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Recademy.Application.Services.Implementations;

public class ProjectService : IProjectService
{
    private readonly RecademyContext _context;

    public ProjectService(RecademyContext context)
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

    public ProjectInfoDto AddProject(AddProjectDto arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);

        var newProject = new ProjectInfo
        {
            AuthorId = arguments.UserId,
            GithubLink = arguments.ProjectUrl,
            Title = arguments.ProjectName,
            Skills = arguments.Tags
                .Select(tag => new ProjectSkill { SkillName = tag })
                .ToList()
        };

        _context.ProjectInfos.Add(newProject);
        _context.SaveChanges();

        return newProject.ToDto();
    }
}