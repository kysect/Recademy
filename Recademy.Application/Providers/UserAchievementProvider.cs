using Recademy.Core.Models.Achievements;
using System.Collections.Generic;
using System.Linq;

namespace Recademy.Application.Providers;

public static class UserAchievementProvider
{
    public static readonly IReadOnlyCollection<IUserAchievement> Achievements = new List<IUserAchievement>
    {
        new FirstTimeUserAchievement(),
        new NeatUserAchievement(),
    };

    public static IUserAchievement FindAchievementById(int achievementId)
    {
        return Achievements.FirstOrDefault(achievement => achievement.Id == achievementId);
    }
}