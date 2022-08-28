using System.Collections.Generic;

namespace Recademy.Shared.Dtos.Projects
{
    public class TagProfileDto
    {
        public string TagName { get; set; }
        public List<ProjectInfoDto> Projects { get; set; }
    }
}