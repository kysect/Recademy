using System.Collections.Generic;
using Recademy.BlazorWeb.Dto;
using Recademy.BlazorWeb.Models;
using Recademy.BlazorWeb.Services.Abstraction;

namespace Recademy.BlazorWeb.Services
{
    public class AchievementService : IAchievementService
    {
        public List<AchievementsDto> GetAchievements(User userInfo)
        {
            var achievements = new List<AchievementsDto>();

            //TODO: add IAchievement with add properties and method 'bool Check(User user)'
            if (userInfo.ReviewRequests.Count >= 1)
                achievements.Add(new AchievementsDto
                {
                    Name = "First time",
                    Description = "You did your first request, and we gave u some goods :)",
                    Icon = "repeat_one"
                });

            if (userInfo.ProjectInfos.Count >= 3)
                achievements.Add(new AchievementsDto
                {
                    Name = "3 projects",
                    Description = "You have at least 3 projects!",
                    Icon = "alarm_on"
                });

            if (userInfo.UserSkills.Count >= 1)
                achievements.Add(new AchievementsDto
                {
                    Name = "U skilled",
                    Description = "You have skilled at least in 1 technology, u so good!",
                    Icon = "favorite_border"
                });

            if (userInfo.UserSkills.Count >= 3)
                achievements.Add(new AchievementsDto
                {
                    Name = "so smart",
                    Description = "You have skilled at least in 3 technologies, good job",
                    Icon = "extension"
                });

            return achievements;
        }
    }
}