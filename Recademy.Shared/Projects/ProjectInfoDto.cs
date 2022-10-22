using Recademy.Dto.Skills;
using System.Collections.Generic;

namespace Recademy.Dto.Projects;

public class ProjectInfoDto
{
    public ProjectInfoDto()
    {
    }

    public ProjectInfoDto(int userId, string username, int projectId, string projectName, string projectUrl, IReadOnlyCollection<ProjectSkillDto> projectSkills)
    {
        UserId = userId;
        Username = username;
        ProjectId = projectId;
        ProjectName = projectName;
        ProjectUrl = projectUrl;
        ProjectSkills = projectSkills;
    }

    public int UserId { get; init; }
    public string Username { get; init; }
    public int ProjectId { get; init; }
    public string ProjectName { get; init; }
    public string ProjectUrl { get; init; }
    public IReadOnlyCollection<ProjectSkillDto> ProjectSkills { get; init; }
}