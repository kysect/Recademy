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
        public string GetSkillName()
        {
            var randVal = _random.Next(0, _skills.Count);
            return _skills[randVal];
        }

        public int GetRandomTechnology()
        {
            return _random.Next(_skills.Count);
        }

        public List<Skill> GetTechnologiesList()
        {
            List<Skill> techsList = new List<Skill>();
            foreach (var el in _skills)
            {
                techsList.Add(new Skill() {Name = el, Description = "some description"});
            }
            return techsList;
        }
    }
}
