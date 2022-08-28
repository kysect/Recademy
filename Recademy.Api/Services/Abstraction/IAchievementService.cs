using System.Collections.Generic;
using Recademy.Shared.Dtos;
using Recademy.Core.Models;

namespace Recademy.Api.Services.Abstraction
{
    public interface IAchievementService
    {
        List<AchievementsDto> GetAchievements(User userInfo);
        List<int> GetUserActivityPerMonth(int userId);
    }
}