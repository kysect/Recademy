using System.Collections.Generic;

namespace Recademy.Dto
{
    public class TagProfileDto
    {
        public string TagName { get; set; }
        public List<ProjectDto> Projects { get; set; }
    }
}