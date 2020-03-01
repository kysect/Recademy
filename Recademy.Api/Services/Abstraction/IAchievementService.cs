using System.Collections.Generic;
using Recademy.Library.Dto;
using Recademy.Library.Models;

namespace Recademy.Api.Services.Abstraction
{
    public interface IAchievementService
    {
        List<AchievementsDto> GetAchievements(User userInfo);
    }
}