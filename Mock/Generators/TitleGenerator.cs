using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mock.Generators
{
    public class TitleGenerator
    {
        private List<string> _titles = new List<string>()
        {
            "VK-music-bot-for-discord","VkQueue","Recademy","ReviewYourself","AlgorithmsAndStructures",
            "LinearAlgebra","DCLab2","Algorithms-2018-2019","OOPThirdSemestr","OSLabs","CourseLab","GameOfLife","is-arch-lect",
            "Fluda","ScheduleAggregator","roslyn", "CodeforcesApiWrapper", "ITMO-Physics-2th-sem", "GeneticCell"
        };

        private readonly Random _random = new Random();
        
        public string GetTitle()
        {
            var randVal = _random.Next(_titles.Count);
            return _titles[randVal];    
        }
    }
}
