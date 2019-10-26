using Recademy.Models;

namespace Recademy.Dto
{
    public class ProjectDto
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectUrl { get; set; }

        public static ProjectDto Of(ProjectInfo projectInfo)
        {
            return new ProjectDto
            {
                ProjectId = projectInfo.Id,
                UserId = projectInfo.AuthorId,
                ProjectName = projectInfo.Title,
                ProjectUrl = projectInfo.GithubLink
            };
        }
    }
}