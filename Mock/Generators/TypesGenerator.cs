using System;
using System.Collections.Generic;
using System.Linq;
using Recademy.Models;
using Recademy.Types;

namespace Mock.Generators
{
    public class TypesGenerator
    {
        private readonly Random _random = new Random();

        private readonly PrimitiveGenerator _primitiveGenerator = new PrimitiveGenerator();

        public User GetUser()
        {
            Utilities.CurrentUserId++;

            return new User
            {
                Name = _primitiveGenerator.GetName(),
                GithubLink = _primitiveGenerator.GetGithubLink()
            };
        }

        /// <summary>
        ///     Get a project info, DON'T GENERATE IT BEFORE GENERATING USERS
        /// </summary>
        /// <returns></returns>
        public ProjectInfo GetProjectInfo(User user) =>
            new ProjectInfo
            {
                Title = _primitiveGenerator.GetTitle(),
                GithubLink = DataLists.Links[_random.Next(DataLists.Links.Count)],
                AuthorId = user.Id,
                User = user,
                Description = $"some description{_random.Next()}"
            };

        public ReviewResponse GetResponse(ReviewRequest reviewRequest, int reviewerId) =>
            new ReviewResponse
            {
                ReviewRequest = reviewRequest,
                Description = $"Some Description#{_random.Next()}",
                ReviewRequestId = reviewRequest.Id,
                ReviewerId = reviewerId,
                CreationTime = _primitiveGenerator.GetRandomDay()
            };

        public ReviewRequest GetRequest(ProjectInfo project, User user, ProjectState projectState, DateTime? creationTime = null)
        {
            if (creationTime == null)
                creationTime = _primitiveGenerator.GetRandomDay();

            return new ReviewRequest
            {
                User = user,
                DateCreate = (DateTime)creationTime,
                ProjectInfo = project,
                ProjectId = project.Id,
                State = projectState
            };
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