using Recademy.Core.Models;
using Recademy.Shared.Dtos.Achievements;

namespace Recademy.Application.Services.Abstractions
{
    public interface IAchievementService
    {
        List<AchievementsDto> GetAchievements(User userInfo);
        List<int> GetUserActivityPerMonth(int userId);
    }
}