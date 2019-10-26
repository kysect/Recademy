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
            List<int> result = Enumerable.Repeat(0,13).ToList();

            var reviewResponses = Context.ReviewResponses.Where(x => x.ReviewerId == userId);

            int year = DateTime.Now.Year;
            foreach (var el in reviewResponses)
            {
                if (el.ReviewRequest.DateCreate.Year == year)
                    result[el.ReviewRequest.DateCreate.Month]++;
            }

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