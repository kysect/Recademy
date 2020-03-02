using System.Collections.Generic;

namespace Recademy.Library.Dto
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

        public int UserId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectUrl { get; set; }
        public List<string> Tags { get; set; }
    }
}