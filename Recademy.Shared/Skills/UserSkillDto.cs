using Recademy.Dto.Users;

namespace Recademy.Dto.Skills;

public class UserSkillDto
{
    public UserSkillDto()
    {
    }

    public RecademyUserDto User { get; init; }
    public SkillDto Skill { get; init; }
}