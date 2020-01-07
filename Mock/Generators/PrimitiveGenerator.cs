using System;
using System.Collections.Generic;
using Mock.Utility;

namespace Mock.Generators
{
    public class PrimitiveGenerator
    {
        private readonly Random _random = new Random();
        public int GetId()
        {
            // get a random value in id's range
            // Utilities.CurrentUserId - max value for id
            int randVal = _random.Next(Utilities.CurrentUserId);
            return randVal;
        }

        public string GetName()
        {
            return DataLists.Names.GetRandomValue();
        }


        public string GetGithubLink()
        {
            return DataLists.GitHubLinks.GetRandomValue();
        }

        public string GetTitle()
        {
            return DataLists.Titles.GetRandomValue();
        }

        /// <summary>
        /// Get a random day between current day and start of year
        /// </summary>
        /// <returns></returns>
        public DateTime GetRandomDay()
        {
            DateTime start = new DateTime(DateTime.Now.Year, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(_random.Next(range));
        }

        public string GetSkillName()
        {
            return DataLists.Skills.GetRandomValue();
        }
    }
}