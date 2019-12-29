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
            /// <summary>
        ///     Get a random skill
        /// </summary>
        /// <returns></returns>
        public string GetSkillName()
        {
            int randVal = _random.Next(0, DataLists.Skills.Count);
            return DataLists.Skills[randVal];
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