using System.ComponentModel.DataAnnotations.Schema;
using Recademy.Core.Models.Users;

namespace Recademy.Core.Models.Skills
{
    public class UserSkill
    {
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Skill")]
        public string SkillName { get; set; }
        public Skill Skill { get; set; }
    }
}