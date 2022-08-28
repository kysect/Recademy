using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Recademy.Core.Models.Skills
{
    public class Skill
    {
        [Key]
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<UserSkill> UserSkills { get; set; }
        public ICollection<ProjectSkill> ProjectSkills { get; set; }
    }
}