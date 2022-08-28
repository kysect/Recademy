using System.ComponentModel.DataAnnotations;

namespace Recademy.Shared.Dtos
{
    public class AddProjectDto
    {
        public AddProjectDto()
        {
            
        }

        public AddProjectDto(string projectName, int userId, string url)
        {
            UserId = userId;
            ProjectUrl = url;
            ProjectName = projectName;
            Tags = new List<string>();
        }

        [Required]
        public int UserId { get; set; }
        [Required]
        public string ProjectName { get; set; }
        [Required]
        public string ProjectUrl { get; set; }
        public List<string> Tags { get; set; }
    }
}