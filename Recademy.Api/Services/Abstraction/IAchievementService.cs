using System.Collections.Generic;
using Recademy.Core.Models;
using Recademy.Shared.Dtos.Achievements;

namespace Recademy.Api.Services.Abstraction
{
    public interface IAchievementService
    {
        List<AchievementsDto> GetAchievements(User userInfo);
        List<int> GetUserActivityPerMonth(int userId);
    }
}