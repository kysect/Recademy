using Recademy.Core.Models.Users;

using System.ComponentModel.DataAnnotations.Schema;

namespace Recademy.Core.Models.Skills;

public class UserSkill
{
    [ForeignKey("User")]
    public int UserId { get; set; }
    public virtual RecademyUser User { get; set; }

    [ForeignKey("Skill")]
    public string SkillName { get; set; }
    public virtual Skill Skill { get; set; }
}