using System.Collections.Generic;
using Recademy.Dto;

namespace Recademy.Services.Abstraction
{
    public interface IAchievementService
    {
        List<AchievementsDto> GetAchievements(int userId);
    }
}