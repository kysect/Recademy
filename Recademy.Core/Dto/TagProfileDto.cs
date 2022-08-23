using System.Collections.Generic;

namespace Recademy.Core.Dto
{
    public class TagProfileDto
    {
        public string TagName { get; set; }
        public List<ProjectInfoDto> Projects { get; set; }
    }
}