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
                GithubLink = "https://github.com/InRedikaWB"
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
                Description = "some description"
            };

        public ReviewResponse GetResponse(ReviewRequest reviewRequest, int reviewerId)
        {
            int randVal = _random.Next();
            return new ReviewResponse
            {
                ReviewRequest = reviewRequest,
                Description = $"Some Description#{randVal}",
                ReviewRequestId = reviewRequest.Id,
                ReviewerId = reviewerId,
                CreationTime = _primitiveGenerator.RandomDay()
            };
        }

        public ReviewRequest GetRequest(ProjectInfo project, User user, int randomId)
        {
            //TODO: check if it's ok
            if (user.Id == randomId)
                return null;

            return new ReviewRequest
            {
                User = user,
                DateCreate = _primitiveGenerator.RandomDay(),
                ProjectInfo = project,
                ProjectId = project.Id,
                State = _random.Next(0, 2) == 0 ? ProjectState.Requested : ProjectState.Reviewed
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