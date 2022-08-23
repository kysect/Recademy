using System;
using System.Collections.Generic;
using System.Linq;
using Recademy.Core.Models;
using Recademy.Core.Types;
using Recademy.Mock.Extensions;

namespace Recademy.Mock.Generators
{
    public class TypesGenerator
    {
        public User GetUser(UserType userType)
        {
            return new User
            {
                Name = DataLists.Names.GetRandomValue(),
                GithubUsername = DataLists.GitHubUsernames.GetRandomValue(),
                UserType = userType
            };
        }

        /// <summary>
        ///     Get a project info, DON'T GENERATE IT BEFORE GENERATING USERS
        /// </summary>
        /// <returns></returns>
        public ProjectInfo GetProjectInfo(User user) =>
            new ProjectInfo
            {
                Title = DataLists.Titles.GetRandomValue(),
                GithubLink = DataLists.ProjectLinks.GetRandomValue(),
                AuthorId = user.Id,
                User = user,
                Description = $"some description{Utilities.Random.Next()}"
            };

        public ReviewResponse GetResponse(ReviewRequest reviewRequest, int reviewerId) =>
            new ReviewResponse
            {
                ReviewRequest = reviewRequest,
                Description = $"Some Description#{Utilities.Random.Next()}",
                ReviewRequestId = reviewRequest.Id,
                ReviewerId = reviewerId,
                CreationTime = GetRandomDay()
            };

        public ReviewRequest GetRequest(ProjectInfo project, User user, ProjectState projectState, DateTime? creationTime = null)
        {
            if (creationTime == null)
                creationTime = GetRandomDay();

            return new ReviewRequest
            {
                User = user,
                DateCreate = creationTime.Value,
                ProjectInfo = project,
                ProjectId = project.Id,
                State = projectState
            };
        }

        /// <summary>
        /// Get a random day between current day and start of year
        /// </summary>
        /// <returns></returns>
        public DateTime GetRandomDay()
        {
            DateTime start = new DateTime(DateTime.Now.Year, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(Utilities.Random.Next(range));
        }

        public List<Skill> GetTechnologiesList()
        {
            return DataLists
                .Skills
                .Select(k =>
                    new Skill
                    {
                        Name = k,
                        Description = "Some description"
                    }).
                ToList();
        }
    }
}