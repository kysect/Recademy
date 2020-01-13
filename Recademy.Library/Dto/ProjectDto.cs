using System.Collections.Generic;
using System.Linq;
using Recademy.Library.Models;

namespace Recademy.Library.Dto
{
    public class ProjectDto
    {
        public ProjectDto(ProjectInfo projectInfo)
        {
            ProjectId = projectInfo.Id;
            UserId = projectInfo.AuthorId;
            ProjectName = projectInfo.Title;
            ProjectUrl = projectInfo.GithubLink;
            ProjectSkills = projectInfo
                .Skills
                .Select(s => s.SkillName)
                .ToList();
        }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectUrl { get; set; }
        public List<string> ProjectSkills { get; set; }
    }
}