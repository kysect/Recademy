using System;

namespace Mock.Generators
{
    public class IdGenerator
    {
        private readonly Random _random = new Random();

        public int GetId()
        {
            // get a random value in id's range
            // Utilities.CurrentUserId - max value for id
            int randVal = _random.Next(Utilities.CurrentUserId);
            return randVal;
        }
    }
}