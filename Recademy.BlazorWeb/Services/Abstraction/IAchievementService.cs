using System.Collections.Generic;
using Recademy.BlazorWeb.Dto;
using Recademy.BlazorWeb.Models;

namespace Recademy.BlazorWeb.Services.Abstraction
{
    public interface IAchievementService
    {
        List<AchievementsDto> GetAchievements(User userInfo);
    }
}