using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Recademy.Core.Models.Skills;

public class Skill
{
    [Key]
    public string Name { get; init; }
    public string Description { get; init; }
    public virtual ICollection<UserSkill> UserSkills { get; init; }
    public virtual ICollection<ProjectSkill> ProjectSkills { get; init; }
}