using System;
using System.Collections.Generic;
using System.Linq;
using Recademy.Models;

namespace Mock.Generators
{
    /// <summary>
    ///     Get a skill, DON'T GENERATE IT BEFORE GENERATING USERS
    /// </summary>
    /// <returns></returns>
    public class SkillGenerator
    {
        private readonly Random _random = new Random();

        private readonly List<string> _skills = new List<string>
        {
            "ASP.NET",
            "MVC",
            "Blazor",
            "Razor Pages",
            "Entity Framework",
            ".NET",
            "Unity",
            "React"
        };

        /// <summary>
        ///     Get a random skill
        /// </summary>
        /// <returns></returns>
        public string GetSkillName()
        {
            int randVal = _random.Next(0, _skills.Count);
            return _skills[randVal];
        }

        public int GetRandomTechnology()
        {
            return _random.Next(_skills.Count);
        }

        public List<Skill> GetTechnologiesList()
        {
            return _skills.Select(k => new Skill {Name = k, Description = "Some description"}).ToList();
        }
    }
}