using System;
using System.Collections.Generic;
using Recademy.Models;

namespace Mock.Generators
{
    public class ProjectInfoesGenerator
    {
        private readonly IdGenerator _idGenerator = new IdGenerator();
        private readonly Random _rnd = new Random();
        private readonly SkillGenerator _skillGenerator = new SkillGenerator();
        private readonly TitleGenerator _titleGenerator = new TitleGenerator();

        private readonly List<string> links = new List<string>
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
            string title = _titleGenerator.GetTitle();

            int authorId = _idGenerator.GetId();

            return new ProjectInfo
            {
                Title = title,
                GithubLink = links[_rnd.Next(links.Count)],
                AuthorId = user.Id,
                User = user,
                Description = "some description"
            };
        }
    }
}