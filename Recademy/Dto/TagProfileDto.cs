using System.Collections.Generic;

namespace Recademy.Dto
{
    public class TagProfileDto
    {
        public string TagName { get; set; }
        public List<ProjectDto> Projects { get; set; }

        public static TagProfileDto Of(string tagName, ProjectDto project)
        {
            return new TagProfileDto
            {
                TagName = tagName,
                Projects = new List<ProjectDto>() { project }
            };
        }

        public static TagProfileDto Of(string tagName, List<ProjectDto> projects)
        {
            return new TagProfileDto
            {
                TagName = tagName,
                Projects = projects
            };
        }
    }
}