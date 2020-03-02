using System.Collections.Generic;

namespace Recademy.Library.Dto
{
    public class TagProfileDto
    {
        public string TagName { get; set; }
        public List<ProjectInfoDto> Projects { get; set; }
    }
}