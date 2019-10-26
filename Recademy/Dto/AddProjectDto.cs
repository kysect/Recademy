using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recademy.Dto
{
    public class AddProjectDto
    {
        public int UserId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectUrl { get; set; }
        public TagsDto Tags { get; set; }

        public static AddProjectDto Of(string projectName, int userId, string url, string tag)
        {
            return new AddProjectDto()
            {
                UserId = userId,
                ProjectUrl = url,
                ProjectName = projectName,
                Tags = TagsDto.Of(tag)
            };
        }
    }
}
