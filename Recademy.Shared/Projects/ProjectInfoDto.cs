using Recademy.Dto.Skills;
using System.Collections.Generic;

namespace Recademy.Dto.Projects;

public class ProjectInfoDto
{
    public ProjectInfoDto()
    {
    }

    public ProjectInfoDto(int userId, string username, int projectId, string title, string description, string link, IReadOnlyCollection<ProjectSkillDto> projectSkills)
    {
        UserId = userId;
        Username = username;
        ProjectId = projectId;
        Title = title;
        Description = description;
        Link = link;
        ProjectSkills = projectSkills;
    }

    public int UserId { get; init; }
    public string Username { get; init; }
    public int ProjectId { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public string Link { get; init; }
    public IReadOnlyCollection<ProjectSkillDto> ProjectSkills { get; init; }
}