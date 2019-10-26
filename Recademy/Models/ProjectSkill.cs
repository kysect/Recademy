using System.ComponentModel.DataAnnotations.Schema;

namespace Recademy.Models
{
    public class ProjectSkill
    {
        [ForeignKey("ProjectInfo")] public int ProjectId { get; set; }
        public ProjectInfo ProjectInfo { get; set; }

        [ForeignKey("Skill")] public string SkillName { get; set; }
        public Skill Skill { get; set; }
    }
}