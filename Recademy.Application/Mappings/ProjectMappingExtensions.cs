using Recademy.Core.Models.Projects;
using Recademy.Dto.Projects;

using System.Linq;

namespace Recademy.Application.Mappings;

public static class ProjectMappingExtensions
{
    public static ProjectInfoDto ToDto(this ProjectInfo project)
    {
        if (project is null)
            return null;

        return new ProjectInfoDto
        {
            ProjectId = project.Id,
            UserId = project.AuthorId,
            Username = project.User?.GithubUsername,
            Title = project.Title,
            Description = project.Description,
            Link = project.GithubLink,
            ProjectSkills = project.Skills.Select(skill => skill.ToDto()).ToList(),
        };
    }

    public static ProjectInfo FromDto(this ProjectInfoDto project)
    {
        if (project is null)
            return null;

        return new ProjectInfo
        {
            Id = project.ProjectId,
            AuthorId = project.UserId,
            Title = project.Title,
            Description = project.Description,
            GithubLink = project.Link,
            Skills = project.ProjectSkills.Select(skill => skill.FromDto()).ToList(),
        };
    }
}