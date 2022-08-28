using Recademy.Core.Models.Achievements;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recademy.Application.Services.Abstractions;

public interface IUserAchievementService
{
    IReadOnlyCollection<IUserAchievement> GetAllAchievements();
    IReadOnlyCollection<IUserAchievement> GetUserAchievements(int userId);
    IReadOnlyCollection<IUserAchievement> GetUserAchievements(string username);
    int GetUserAchievementPoints(int userId);
    Task AddUserAchievement(int userId, int achievementId);
}