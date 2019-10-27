using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Recademy.Context;
using Recademy.Dto;
using Recademy.Models;
using Recademy.Services.Abstraction;

namespace Recademy.Services
{
    public class UserService : IUserService
    {
        public RecademyContext Context;

        public UserService(RecademyContext context)
        {
            Context = context;
        }

        public UserInfoDto GetUserInfoDto(int userId)
        {
            User userInfo = Context.Users
                .Include(s => s.ProjectInfos)
                .ThenInclude(p => p.Skills)
                .Include(s => s.UserSkills)
                .Include(u => u.ReviewRequests)
                .FirstOrDefault(s => s.Id == userId);

            var achievements = new AchievementService();
            List<string> skillNames = userInfo.UserSkills.Select(el => el.SkillName).ToList();

            UserInfoDto result = new UserInfoDto()
            {
                UserName = userInfo.Name,
                Activities = GetActivity(userId),
                Skills = skillNames,
                Achievements = achievements.GetAchievements(userInfo),
                ProjectDtos = userInfo.ProjectInfos.Select(ProjectDto.Of).ToList()
            };

            return result;
        }

        /// <summary>
        /// return a user activity, index is month, value is activity number
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<int> GetActivity(int userId)
        {
            List<int> result = Enumerable.Repeat(0, 12).ToList();

            List<ReviewResponse> reviewList = Context.ReviewResponses
                .Where(x => x.ReviewerId == userId)
                .ToList();

            foreach (ReviewResponse el in reviewList)
            {
                result[el.CreationTime.Month]++;
            }

            return result;
        }

        /// <summary>
        /// get activity in count
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetActivityInCount(int userId)
        {
            List<ReviewResponse> activities = Context.ReviewResponses
                .Where(x => x.ReviewerId == userId)
                .ToList();

            return activities.Count;
        }

        /// <summary>
        /// get a score ranking by user's activities
        /// key is user id, value is activity score
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, int> GetRanking()
        {
            Dictionary<string, int> ranking = new Dictionary<string, int>();
            List<User> users = Context.Users.ToList();

            foreach (User user in users)
            {
                int value = GetActivityInCount(user.Id);
                if (value != 0)
                    ranking[user.Name] = value;
            }

            var result = ranking
                .OrderByDescending(x => x.Value)
                .ToDictionary(r => r.Key, r => r.Value);

            return result;
        }

        public ProjectInfo AddProject(AddProjectDto argues)
        {
            ProjectInfo newProject = new ProjectInfo()
            {
                AuthorId = argues.UserId,
                GithubLink = argues.ProjectUrl,
                Title = argues.ProjectName,
                Skills = argues.Tags.Select(t => new ProjectSkill { SkillName = t }).ToList()
            };

            Context.ProjectInfos.Add(newProject);
            Context.SaveChanges();
            return newProject;
        }
    }
}