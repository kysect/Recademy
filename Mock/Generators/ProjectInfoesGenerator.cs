using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Recademy.Models;

namespace Mock.Generators
{
    public class ProjectInfoesGenerator
    {

        private readonly IdGenerator _idGenerator = new IdGenerator();
        private readonly TitleGenerator _titleGenerator = new TitleGenerator();
        private readonly SkillGenerator _skillGenerator = new SkillGenerator();
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
