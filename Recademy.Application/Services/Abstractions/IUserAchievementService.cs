using Recademy.Core.Models.Achievements;

using System.Collections.Generic;
using System.Threading.Tasks;
using Recademy.Dto.Achievements;

namespace Recademy.Application.Services.Abstractions;

public interface IUserAchievementService
{
    IReadOnlyCollection<IUserAchievement> GetAllAchievements();
    IReadOnlyCollection<IUserAchievement> GetUserAchievements(int userId);
    IReadOnlyCollection<UserAchievementRequestDto> GetUserAchievementRequests(int userId);
    int GetUserAchievementPoints(int userId);
    Task AddUserAchievement(int userId, int achievementId);
    Task AddUserAchievementRequest(UserAchievementRequestDto request);
}