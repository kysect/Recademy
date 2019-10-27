using System.Collections.Generic;

namespace Recademy.Dto
{
    public class AddProjectDto
    {
        public int UserId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectUrl { get; set; }
        public List<string> Tags { get; set; }

        public static AddProjectDto Of(string projectName, int userId, string url, string tag)
        {
            return new AddProjectDto
            {
                UserId = userId,
                ProjectUrl = url,
                ProjectName = projectName,
                Tags = new List<string> { tag }
            };
        }
    }
}
