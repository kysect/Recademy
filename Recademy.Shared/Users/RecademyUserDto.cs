using Recademy.Dto.Achievements;
using Recademy.Dto.Projects;
using Recademy.Dto.Reviews;
using Recademy.Dto.Skills;

using System.Collections.Generic;

namespace Recademy.Dto.Users;

public class RecademyUserDto
{
    public RecademyUserDto()
    {
    }

    public int UserId { get; init; }
    public UserInfoDto User { get; init; }
    public ICollection<UserSkillDto> UserSkills { get; init; }
    public ICollection<ProjectInfoDto> ProjectInfos { get; init; }
    public ICollection<ReviewRequestInfoDto> ReviewRequests { get; init; }
    public ICollection<ReviewResponseInfoDto> ReviewResponses { get; init; }
    public ICollection<UserAchievementDto> UserAchievements { get; init; }
}