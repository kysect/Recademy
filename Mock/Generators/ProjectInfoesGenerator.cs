using System;
using System.Collections.Generic;
using Recademy.Models;

namespace Mock.Generators
{
    public class ProjectInfoesGenerator
    {
        private readonly PrimitiveGenerator _primitiveGenerator = new PrimitiveGenerator();
        private readonly Random _rnd = new Random();
        /// <summary>
        ///     Get a project info, DONT'T GENERATE IT BEFORE GENERATING USERS
        /// </summary>
        /// <returns></returns>
        public ProjectInfo GetProjectInfo(User user) => 
            new ProjectInfo
            {
                Title = _primitiveGenerator.GetTitle(),
                GithubLink = DataLists.Links[_rnd.Next(DataLists.Links.Count)],
                AuthorId = user.Id,
                User = user,
                Description = "some description"
            };
    }
}