using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mock.Generators
{
    public class TitleGenerator
    {
        private readonly string _title = "Title#";

        private readonly Random _random = new Random();
        
        public string GetTitle()
        {
            var randVal = _random.Next();
            return $"{_title}{randVal}";
        }
    }
}
