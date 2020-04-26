using System.Collections.Generic;
using Recademy.Api.Services.Abstraction;
using Recademy.Library.Dto;
using Recademy.Library.Models;

namespace Recademy.Api.Services
{
    public class AchievementService : IAchievementService
    {
        public List<AchievementsDto> GetAchievements(User userInfo)
        {
            var achievements = new List<AchievementsDto>();

            //TODO: add IAchievement with add properties and method 'bool Check(User user)'
            if (userInfo.ReviewRequests.Count >= 1)
                achievements.Add(new AchievementsDto(
                    "First time",
                    "You did your first request, and we gave u some goods :)",
                    "repeat_one"));

            if (userInfo.ProjectInfos.Count >= 3)
                achievements.Add(new AchievementsDto(
                    "3 projects",
                    "You have at least 3 projects!",
                    "alarm_on"));

            if (userInfo.UserSkills.Count >= 1)
                achievements.Add(new AchievementsDto(
                    "U skilled",
                    "You have skilled at least in 1 technology, u so good!",
                    "favorite_border"));

            if (userInfo.UserSkills.Count >= 3)
                achievements.Add(new AchievementsDto(
                    "so smart",
                    "You have skilled at least in 3 technologies, good job",
                    "extension"));

            return achievements;
        }
    }
}