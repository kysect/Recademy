using Recademy.Core.Models.Achievements;
using System.Collections.Generic;
using System.Threading.Tasks;
using Recademy.Dto.Achievements;

namespace Recademy.Application.Services.Abstractions;

public interface IUserAchievementService
{
    IReadOnlyCollection<IUserAchievement> GetAllAchievements();
    IReadOnlyCollection<IUserAchievement> GetUserAchievements(int userId);
    IReadOnlyCollection<UserAchievementPointsDto> GetRangesUserAchievements();
    Task<IReadOnlyCollection<UserAchievementRequestDto>> GetUserAchievementRequests();
    IReadOnlyCollection<UserAchievementRequestDto> GetUserAchievementRequests(int userId);
    Task<UserAchievementRequestDto> GetUserAchievementRequestById(int requestId);
    int GetUserAchievementPoints(int userId);
    Task<UserAchievementResponseDto> GetUserAchievementResponse(int requestId);
    Task AddUserAchievement(int userId, int achievementId);
    Task AddUserAchievementRequest(UserAchievementRequestDto request);
    Task AddUserAchievementResponse(UserAchievementResponseDto response);
}