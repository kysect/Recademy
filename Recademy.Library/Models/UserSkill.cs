using System.ComponentModel.DataAnnotations.Schema;

namespace Recademy.Library.Models
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