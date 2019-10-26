using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Recademy.Context;
using Recademy.Services.Abstraction;

namespace Recademy.Services
{
    public class Achievement
    {
        public string Name;

        public string Description;
    }

    public class AchievementService : IAchievementService
    {
        public RecademyContext Context;

        public AchievementService(RecademyContext context)
        {
            Context = context;
        }

        public List<Achievement> GetAchievements(int userId)
        {
            List<Achievement> achievements = new List<Achievement>();
            UserService userService = new UserService(Context);
            var user = userService.GetUserInfo(userId);

            if (user.ReviewRequests.Count >= 1)
            {
                achievements.Add(new Achievement() { Name = "First Request!", Description = "You did your first request, and we gave u some goods :)" });
            }

            if (user.ProjectInfos.Count >= 3)
            {
                achievements.Add(new Achievement()
                { Name = "3 projects", Description = "You have at least 3 projects!" });
            }


            if (user.UserSkills.Count >= 3)
            {
                achievements.Add(new Achievement() { Name = "So experienced", Description = "You have skilled at least in 3 techologies, good job" });
            }
            else if (user.UserSkills.Count >= 1)
            {
                achievements.Add(new Achievement() { Name = "You skilled!", Description = "You have skilled at least in 1 technology, u so good!" });
            }

            return achievements;
        }
    }
}
