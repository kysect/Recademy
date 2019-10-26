using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recademy.Models
{
    public class Skill
    {
        public Skill()
        {
            UserSkills = new List<UserSkill>();
        }

        [Key]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<UserSkill> UserSkills { get; set; }
    }
}
