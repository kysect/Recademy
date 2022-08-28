using System.Collections.Generic;

namespace Recademy.Shared.Dtos.Projects
{
    public class ProjectInfoDto
    {
        public ProjectInfoDto()
        {
        }
        
        public ProjectInfoDto(int userId, int projectId, string projectName, string projectUrl, List<string> projectSkills)
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
        public List<string> ProjectSkills { get; init; }
    }
}