using System;
using System.Collections.Generic;

namespace Mock.Generators
{
    public class TitleGenerator
    {
        private readonly Random _random = new Random();

        private readonly List<string> _titles = new List<string>
        {
            "VK-music-bot-for-discord",
            "VkQueue",
            "Recademy",
            "ReviewYourself",
            "AlgorithmsAndStructures",
            "LinearAlgebra",
            "DCLab2",
            "Algorithms-2018-2019",
            "OOPThirdSemestr",
            "OSLabs",
            "CourseLab",
            "GameOfLife",
            "is-arch-lect",
            "Fluda",
            "ScheduleAggregator",
            "roslyn",
            "CodeforcesApiWrapper",
            "ITMO-Physics-2th-sem",
            "GeneticCell"
        };

        public string GetTitle()
        {
            int randVal = _random.Next(_titles.Count);
            return _titles[randVal];
        }
    }
}