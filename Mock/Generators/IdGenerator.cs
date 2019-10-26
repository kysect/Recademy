using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mock.Generators
{
    public class IdGenerator
    {
        private readonly Random _random = new Random();
        public int GetId()
        {
            // get a random value in id's range
            // Utilities.CurrentUserId - max value for id
            var randVal = _random.Next(Utilities.CurrentUserId);
            return randVal;
        }
    }
}
