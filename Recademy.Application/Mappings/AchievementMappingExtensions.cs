using Recademy.Core.Models.Achievements;
using Recademy.Dto.Achievements;

namespace Recademy.Application.Mappings;

public static class AchievementMappingExtensions
{
    public static UserAchievementDto ToDto(this UserAchievementInfo achievement)
    {
        if (achievement is null)
            return null;

        return new UserAchievementDto
        {
            Id = achievement.AchievementId,
            // TODO: set other parameters
        };
    }

    public static UserAchievementInfo FromDto(this UserAchievementDto achievement, int recademyUserId)
    {
        if (achievement is null)
            return null;

        return new UserAchievementInfo
        {
            UserId = recademyUserId,
            AchievementId = achievement.Id,
        };
    }
}