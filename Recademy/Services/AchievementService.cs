using System.Collections.Generic;
using Recademy.Dto;
using Recademy.Models;
using Recademy.Services.Abstraction;

namespace Recademy.Services
{
    public class AchievementService : IAchievementService
    {
        public List<AchievementsDto> GetAchievements(User userInfo)
        {
            List<AchievementsDto> achievements = new List<AchievementsDto>();

            if (userInfo.ReviewRequests.Count >= 1)
            {
                achievements.Add(new AchievementsDto() { Name = "First Request!", Description = "You did your first request, and we gave u some goods :)" });
            }

            if (userInfo.ProjectInfos.Count >= 3)
            {
                achievements.Add(new AchievementsDto()
                { Name = "3 projects", Description = "You have at least 3 projects!" });
            }


            if (userInfo.UserSkills.Count >= 3)
            {
                achievements.Add(new AchievementsDto() { Name = "So experienced", Description = "You have skilled at least in 3 techologies, good job" });
            }
            else if (userInfo.UserSkills.Count >= 1)
            {
                achievements.Add(new AchievementsDto() { Name = "You skilled!", Description = "You have skilled at least in 1 technology, u so good!" });
            }

            return achievements;
        }
    }
}
