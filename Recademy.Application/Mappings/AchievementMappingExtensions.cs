using Recademy.Core.Models.Achievements;
using Recademy.Dto.Achievements;

namespace Recademy.Application.Mappings;

public static class AchievementMappingExtensions
{
    public static UserAchievementDto ToDto(this UserAchievementInfo achievement)
    {
        return new UserAchievementDto
        {
            Id = achievement.AchievementId,
            // TODO: set other parameters
        };
    }

    public static UserAchievementInfo FromDto(this UserAchievementDto achievement, int recademyUserId)
    {
        return new UserAchievementInfo
        {
            UserId = recademyUserId,
            AchievementId = achievement.Id,
        };
    }
}