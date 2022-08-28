using Recademy.Core.Models.Projects;
using Recademy.Core.Tools;
using Recademy.Shared.Dtos.Projects;

namespace Recademy.Application.Mappings;

public static class ProjectMappingExtensions
{
    public static ProjectInfoDto ToDto(this ProjectInfo project)
    {
        return new ProjectInfoDto
        {
            ProjectId = project.Id,
            UserId = project.AuthorId,
            ProjectName = project.Title,
            ProjectUrl = project.GithubLink,
            ProjectSkills = project.Skills.Maybe(s => s.SkillName),
        };
    }
}