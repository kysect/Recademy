using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Recademy.Api.Services.Abstraction;
using Recademy.Core.Models;
using Recademy.Core.Models.Achievements;

namespace Recademy.Api.Services.Implementations;

public sealed class UserAchievementService : IUserAchievementService
{
    private readonly IReadOnlyCollection<IUserAchievement> _achievements = new List<IUserAchievement>()
    {
        new FirstTimeUserAchievement(),
        new NeatUserAchievement(),
    };

    private readonly RecademyContext _context;

    public UserAchievementService(RecademyContext context)
    {
        _context = context;
    }

    public IReadOnlyCollection<IUserAchievement> GetAllAchievements()
    {
        return _achievements;
    }

    public IReadOnlyCollection<IUserAchievement> GetUserAchievements(Int32 userId)
    {
        HashSet<int> userAchievements = _context.UserAchievementInfos
            .Where(achievement => achievement.UserId == userId)
            .Select(achievement => achievement.AchievementId)
            .ToHashSet();

        return _achievements
            .Where(achievement => userAchievements.Contains(achievement.Id))
            .ToList();
    }

    public IReadOnlyCollection<IUserAchievement> GetUserAchievements(String username)
    {
        throw new NotImplementedException();
    }

    public Int32 GetUserAchievementPoints(int userId)
    {
        return GetUserAchievements(userId).Sum(achievement => achievement.Points);
    }

    public async Task AddUserAchievement(Int32 userId, Int32 achievementId)
    {
        _context.UserAchievementInfos.Add(new UserAchievementInfo() {AchievementId = achievementId, UserId = userId});
        await _context.SaveChangesAsync();
    }
}