using Recademy.Core.Models;
using Recademy.Core.Tools;

namespace Recademy.Shared.Dtos
{
    public class ProjectInfoDto
    {
        public ProjectInfoDto()
        {
            
        }

        public ProjectInfoDto(ProjectInfo projectInfo)
        {
            ProjectId = projectInfo.Id;
            UserId = projectInfo.AuthorId;
            ProjectName = projectInfo.Title;
            ProjectUrl = projectInfo.GithubLink;
            ProjectSkills = projectInfo.Skills.Maybe(s => s.SkillName);
        }

        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectUrl { get; set; }
        public List<string> ProjectSkills { get; set; }
    }
}