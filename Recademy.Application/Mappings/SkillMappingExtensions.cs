using Recademy.Core.Models.Skills;
using Recademy.Dto.Skills;

namespace Recademy.Application.Mappings;

public static class SkillMappingExtensions
{
    public static SkillDto ToDto(this Skill skill)
    {
        if (skill is null)
            return null;

        return new SkillDto
        {
            Name = skill.Name,
            Description = skill.Description,
        };
    }

    public static Skill FromDto(this SkillDto skill)
    {
        if (skill is null)
            return null;

        return new Skill
        {
            Name = skill.Name,
            Description = skill.Description,
        };
    }

    public static UserSkillDto ToDto(this UserSkill userSkill)
    {
        if (userSkill is null)
            return null;

        return new UserSkillDto
        {
            User = userSkill.User.ToDto(),
            Skill = userSkill.Skill.ToDto(),
        };
    }

    public static UserSkill FromDto(this UserSkillDto userSkill)
    {
        if (userSkill is null)
            return null;

        return new UserSkill
        {
            UserId = userSkill.User.UserId,
            SkillName = userSkill.Skill.Name,
            Skill = userSkill.Skill.FromDto(),
        };
    }

    public static ProjectSkillDto ToDto(this ProjectSkill projectSkill)
    {
        if (projectSkill is null)
            return null;

        return new ProjectSkillDto
        {
            ProjectId = projectSkill.ProjectId,
            Skill = projectSkill.Skill.ToDto(),
        };
    }
    public static ProjectSkill FromDto(this ProjectSkillDto projectSkill)
    {
        if (projectSkill is null)
            return null;

        return new ProjectSkill
        {
            ProjectId = projectSkill.ProjectId,
            SkillName = projectSkill.Skill.Name,
            Skill = projectSkill.Skill.FromDto(),
        };
    }
}