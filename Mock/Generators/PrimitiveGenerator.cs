using System;
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

        private readonly List<string> _names = new List<string>
        {
            "Norah",
            "Gibson",
            "Fleur",
            "Alcock",
            "Aneurin",
            "Hansen",
            "Jason",
            "Hadassah",
            "Davison",
            "Siobhan",
            "Aedan",
            "Layla",
            "Raul",
            "Annika",
            "Blaine",
            "Kobe",
            "Amna",
            "Laiba",
            "Kimberly",
            "Lennox",
            "Jeanne",
            "Valerie",
            "Peyton",
            "Arias",
            "Alvarez",
            "Berg",
            "Barclay",
            "Galindo",
            "Poole",
            "Wilkins"
        };
        public string GetName()
        {
            int randVal = _random.Next(0, _names.Count);
            return _names[randVal];
        }

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