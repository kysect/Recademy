using System;
using System.Collections.Generic;

namespace Mock.Generators
{
    public class NameGenerator
    {
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

        private readonly Random _random = new Random();

        /// <summary>
        ///     get a random name
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            int randVal = _random.Next(0, _names.Count);
            return _names[randVal];
        }
    }
}