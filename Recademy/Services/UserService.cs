using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
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

        public User GetUserInfo(int userId)
        {
            User userInfo = Context.Users
                .Include(s => s.ProjectInfos)
                .Include(s => s.UserSkills)
                .Include(u => u.ReviewRequests)
                .FirstOrDefault(s => s.Id == userId);

            return userInfo;
        }

        public UserInfoDto GetUserInfoDto(int userId)
        {
            User userInfo = Context.Users
                .Include(s => s.ProjectInfos)
                .Include(s => s.UserSkills)
                .Include(u => u.ReviewRequests)
                .FirstOrDefault(s => s.Id == userId);

            List<string> skillNames = new List<string>();

            AchievementService achievements = new AchievementService(Context);

            skillNames = userInfo.UserSkills.Select(el => el.SkillName).ToList();

            List<ProjectDto> projectInfos = new List<ProjectDto>();

            foreach (var el in userInfo.ProjectInfos)
            {
                projectInfos.Add(ProjectDto.Of(el));
            }

            UserInfoDto result = new UserInfoDto()
            {
                UserName = userInfo.Name,
                Activities = GetActivity(userId),
                Skills = skillNames,
                Achievements = achievements.GetAchievements(userId),
                ProjectDtos = projectInfos
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
            List<int> result = Enumerable.Repeat(0, 13).ToList();

            var reviewResponses = Context.ReviewResponses.Where(x => x.ReviewerId == userId).ToList();

            int year = DateTime.Now.Year;
            foreach (var el in reviewResponses)
            {
                var reviewRequest = Context.ReviewRequests.Where(x => x.Id == el.ReviewRequestId).ToList().FirstOrDefault();

                if (reviewRequest == null)
                    continue;

                if (reviewRequest.DateCreate.Year == year)
                    result[el.ReviewRequest.DateCreate.Month]++;
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
            int count = Context.ReviewResponses
                .Where(x => x.ReviewerId == userId)
                .Count(r => r.CreationTime.Year == DateTime.Now.Year);

            return count;
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
                Skills = argues.Tags.Select(t => new ProjectSkill() { SkillName = t }).ToList()
            };

            Context.ProjectInfos.Add(newProject);
            Context.SaveChanges();
            return newProject;
        }
    }
}