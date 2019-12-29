using System;
using System.Collections.Generic;
using Recademy.Models;

namespace Mock.Generators
{
    public class ProjectInfoesGenerator
    {
        private readonly PrimitiveGenerator _primitiveGenerator = new PrimitiveGenerator();
        private readonly Random _rnd = new Random();

        private readonly List<string> _links = new List<string>
        {
            "https://github.com/riiji/VkQueue",
            "https://github.com/riiji/VK-music-bot-for-discord",
            "https://github.com/riiji/RPG",
            "https://github.com/TEF-Y/Main",
            "https://github.com/InRedikaWB/Fluda",
            "https://github.com/InRedikaWB/VkLibrary",
            "https://github.com/InRedikaWB/is-arch-lect",
            "https://github.com/InRedikaWB/CodeforcesApiWrapper"
        };

        /// <summary>
        ///     Get a project info, DONT'T GENERATE IT BEFORE GENERATING USERS
        /// </summary>
        /// <returns></returns>
        public ProjectInfo GetProjectInfo(User user)
        {
            string title = _primitiveGenerator.GetTitle();

            return new ProjectInfo
            {
                Title = title,
                GithubLink = _links[_rnd.Next(_links.Count)],
                AuthorId = user.Id,
                User = user,
                Description = "some description"
            };
        }
    }
}