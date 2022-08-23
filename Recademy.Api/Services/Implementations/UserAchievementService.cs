using System.Collections.Generic;
using Recademy.Api.Services.Abstraction;
using Recademy.Core.Models.Achievements;

namespace Recademy.Core;

public sealed class UserAchievementService : IUserAchievementService
{
    private readonly IReadOnlyCollection<IUserAchievement> _achievements = new List<IUserAchievement>()
    {
        new FirstTimeUserAchievement(),
        new NeatUserAchievement(),
    };

    public IReadOnlyCollection<IUserAchievement> GetAllAchievements()
    {
        return _achievements;
    }
}