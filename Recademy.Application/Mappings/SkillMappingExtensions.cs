using Recademy.Core.Models.Skills;
using Recademy.Dto.Skills;

using System.Linq;

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
            ProjectSkills = skill.ProjectSkills
                .Select(projectSkill => projectSkill.ToDto())
                .ToList(),
            UserSkills = skill.UserSkills
                .Select(userSkill => userSkill.ToDto())
                .ToList(),
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
            ProjectSkills = skill.ProjectSkills
                .Select(projectSkill => projectSkill.FromDto())
                .ToList(),
            UserSkills = skill.UserSkills
                .Select(userSkill => userSkill.FromDto())
                .ToList(),
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
            Project = projectSkill.ProjectInfo.ToDto(),
            Skill = projectSkill.Skill.ToDto(),
        };
    }
    public static ProjectSkill FromDto(this ProjectSkillDto projectSkill)
    {
        if (projectSkill is null)
            return null;

        return new ProjectSkill
        {
            ProjectId = projectSkill.Project.ProjectId,
            SkillName = projectSkill.Skill.Name,
            Skill = projectSkill.Skill.FromDto(),
        };
    }
}