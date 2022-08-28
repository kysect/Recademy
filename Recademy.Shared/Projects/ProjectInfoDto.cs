using Recademy.Dto.Skills;

using System.Collections.Generic;

namespace Recademy.Dto.Projects
{
    public class ProjectInfoDto
    {
        public ProjectInfoDto()
        {
        }

        public ProjectInfoDto(int userId, int projectId, string projectName, string projectUrl, List<ProjectSkillDto> projectSkills)
        {
            UserId = userId;
            ProjectId = projectId;
            ProjectName = projectName;
            ProjectUrl = projectUrl;
            ProjectSkills = projectSkills;
        }

        public int UserId { get; init; }
        public int ProjectId { get; init; }
        public string ProjectName { get; init; }
        public string ProjectUrl { get; init; }
        public List<ProjectSkillDto> ProjectSkills { get; init; }
    }
}