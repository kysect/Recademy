﻿using System;
using System.Collections.Generic;

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
            int randVal = _random.Next(0, DataLists.Names.Count);
            return DataLists.Names[randVal];
        }


        public string GetTitle()
        {
            int randVal = _random.Next(DataLists.Titles.Count);
            return DataLists.Titles[randVal];
        }

        public DateTime RandomDay()
        {
            DateTime start = new DateTime(DateTime.Now.Year, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(_random.Next(range));
        }

        public string GetSkillName()
        {
            int randVal = _random.Next(0, DataLists.Skills.Count);
            return DataLists.Skills[randVal];
        }
    }
}