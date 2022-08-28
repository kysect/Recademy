using System.Collections.Generic;
using System.Threading.Tasks;
using Recademy.Core.Models.Achievements;

namespace Recademy.Application.Services.Abstractions;

public interface IUserAchievementService
{
    IReadOnlyCollection<IUserAchievement> GetAllAchievements();
    IReadOnlyCollection<IUserAchievement> GetUserAchievements(int userId);
    IReadOnlyCollection<IUserAchievement> GetUserAchievements(string username);
    int GetUserAchievementPoints(int userId);
    Task AddUserAchievement(int userId, int achievementId);
}