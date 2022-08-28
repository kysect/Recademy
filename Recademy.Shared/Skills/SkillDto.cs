using System.Collections.Generic;

namespace Recademy.Dto.Skills;

public class SkillDto
{
    public SkillDto()
    {
    }

    public string Name { get; init; }
    public string Description { get; init; }

    public ICollection<UserSkillDto> UserSkills { get; init; }
    public ICollection<ProjectSkillDto> ProjectSkills { get; init; }
}