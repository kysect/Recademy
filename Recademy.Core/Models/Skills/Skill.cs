using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Recademy.Core.Models.Skills;

public class Skill
{
    [Key]
    public string Name { get; init; }
    public string Description { get; init; }

    public ICollection<UserSkill> UserSkills { get; init; }
    public ICollection<ProjectSkill> ProjectSkills { get; init; }
}