using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Recademy.Models;
using User = Recademy.Models.User;

namespace Mock.Generators
{
    public class ProjectInfoesGenerator
    {
        
        private List<string> links = new List<string>() { "https://github.com/riiji/VkQueue", "https://github.com/riiji/VK-music-bot-for-discord", "https://github.com/riiji/RPG" };

        private readonly IdGenerator _idGenerator = new IdGenerator();
        private readonly TitleGenerator _titleGenerator = new TitleGenerator();
        private readonly SkillGenerator _skillGenerator = new SkillGenerator();
        private readonly Random _rnd = new Random();
        /// <summary>
        /// Get a project info, DONT'T GENERATE IT BEFORE GENERATING USERS
        /// </summary>
        /// <returns></returns>
        public ProjectInfo GetProjectInfo(User user)
        {
            string title = _titleGenerator.GetTitle();

            int authorId = _idGenerator.GetId();

            ProjectInfo result = new ProjectInfo() { Title = title, GithubLink = "https://github.com/InRedikaWB", AuthorId = user.Id, User = user, Description = "some description"};

            return result;
        }
    }
}
