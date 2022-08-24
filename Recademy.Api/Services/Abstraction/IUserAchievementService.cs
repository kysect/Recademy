using System.Collections.Generic;
using System.Threading.Tasks;

using Recademy.Core.Models.Achievements;

namespace Recademy.Api.Services.Abstraction;

public interface IUserAchievementService
{
    IReadOnlyCollection<IUserAchievement> GetAllAchievements();
    IReadOnlyCollection<IUserAchievement> GetUserAchievements(int userId);
    IReadOnlyCollection<IUserAchievement> GetUserAchievements(string username);
    Task AddUserAchievement(int userId, int achievementId);
}