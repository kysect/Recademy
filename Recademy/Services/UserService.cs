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

        public User GetUserInfo(int userId)
        {
            User userInfo = Context.Users
                .Include(s => s.ProjectInfos)
                .Include(s => s.UserSkills)
                .FirstOrDefault(s => s.Id == userId);

            return userInfo;
        }

        public Dictionary<int, int> GetActivity(int userId)
        {
            Dictionary<int,int> result = new Dictionary<int, int>();
            var reviewRequests = Context.ReviewRequests.Where(x => x.Id == userId);
            int year = DateTime.Now.Year;

            foreach (var el in reviewRequests)
            {
                // if date create == current year
                // put into activity dictionary
                if (el.DateCreate.Year == year)
                {
                    result[el.DateCreate.Month]++;
                }
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