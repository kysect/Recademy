using System.Collections.Generic;

namespace Recademy.Dto.Projects
{
    public class TagProfileDto
    {
        public string TagName { get; set; }
        public List<ProjectInfoDto> Projects { get; set; }
    }
}