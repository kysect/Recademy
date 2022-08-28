using System.ComponentModel.DataAnnotations;

namespace Recademy.Dto.Github
{
    public class GitHubIssueCreateDto
    {
        [Required]
        public string OwnerLogin { get; set; }
        [Required]
        public string RepositoryName { get; set; }
        [Required]
        public string IssueTitle { get; set; }
        [Required]
        public string IssueText { get; set; }
    }
}