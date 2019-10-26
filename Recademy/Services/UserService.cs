using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Recademy.Context;
using Recademy.Dto;
using Recademy.Models;

namespace Recademy.Services
{
    public class UserService
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
                .FirstOrDefault(s => s.Id == userId);

            return userInfo;
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
            int count = 0;

            var reviewResponses = Context.ReviewResponses.Where(x => x.ReviewerId == userId).ToList();
            
            int year = DateTime.Now.Year;
            foreach (var el in reviewResponses)
            {
                var reviewRequest = Context.ReviewRequests.Where(x => x.Id == el.ReviewRequestId).ToList().FirstOrDefault();

                if(reviewRequest==null)
                    continue;

                if (reviewRequest.DateCreate.Year == year)
                   count++;
            }

            return count;
        }
        /// <summary>
        /// get a score ranking by user's activities
        /// key is user id, value is activity score
        /// </summary>
        /// <returns></returns>
        public List<KeyValuePair<int,int>> GetRanking()
        {
            List<KeyValuePair<int, int>> ranking = new List<KeyValuePair<int, int>>();

            var users = Context.Users.ToList();

            foreach (var user in users)
            {
                var value = GetActivityInCount(user.Id);
                if (value != 0)
                    ranking.Add(new KeyValuePair<int, int>(user.Id, value));
            }

            var result = ranking.OrderByDescending(x => x.Value).ToList();

            return result;
        }

        public ProjectInfo AddProject(AddProjectDto argues)
        {
            ProjectInfo newProject = new ProjectInfo()
            {
                AuthorId = argues.UserId,
                GithubLink = argues.ProjectUrl,
                Title = argues.ProjectName
            };

            Context.ProjectInfos.Add(newProject);
            Context.SaveChanges();
            return newProject;
        }
    }
}