using Recademy.Dto.Projects;

namespace Recademy.Dto.Skills;

public class ProjectSkillDto
{
    public ProjectSkillDto()
    {
    }

    public ProjectInfoDto Project { get; init; }
    public SkillDto Skill { get; init; }
}