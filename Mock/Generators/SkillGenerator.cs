using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Recademy.Models;

namespace Mock.Generators
{
    /// <summary>
    /// Get a skill, DON'T GENERATE IT BEFORE GENERATING USERS
    /// </summary>
    /// <returns></returns>
    public class SkillGenerator
    {
        private readonly List<string> _skills = new List<string>()
        {
            "ASP.NET","MVC","Blazor","Razor Pages", "Entity Framework", ".NET", "Unity", "React"
        };

        private readonly Random _random = new Random();

        /// <summary>
        /// Get a random skill
        /// </summary>
        /// <returns></returns>
        public Skill GetSkill()
        {
            var randVal = _random.Next(0, _skills.Count);
            Skill result = new Skill() { Name = _skills[randVal], Description = "some description for skill"};
            return result;
        }
    }
}
