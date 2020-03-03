using System.Collections.Generic;
using System.Linq;
using Recademy.Library.Models;
using Recademy.Library.Tools;

namespace Recademy.Library.Dto
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