using System.Collections.Generic;
using Recademy.Dto;
using Recademy.Models;

namespace Recademy.Services.Abstraction
{
    public interface IAchievementService
    {
        List<AchievementsDto> GetAchievements(User userInfo);
    }
}