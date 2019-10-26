using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mock.Generators
{
    public class NameGenerator
    {
        private readonly List<string> _names = new List<string>()
        {
            "Norah", "Gibson","Fleur","Alcock","Aneurin","Hansen","Jason","Hadassah","Davison","Siobhan",
            "Aedan","Layla","Raul","Annika","Blaine","Kobe","Amna","Laiba","Kimberly","Lennox","Jeanne",
            "Valerie","Peyton","Arias","Alvarez","Berg","Barclay","Galindo","Poole","Wilkins"
        };

        private readonly Random _random = new Random();

        /// <summary>
        /// get a random name
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            var randVal = _random.Next(0, _names.Count);
            return _names[randVal];
        }
    }
}
