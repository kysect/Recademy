using System.ComponentModel.DataAnnotations;

namespace Recademy.Library.Dto
{
    public class GitHubIssueCreateDto
    {
        [Required]
        public string ProjectUrl { get; set; }

        [Required]
        public string IssueText { get; set; }
    }
}