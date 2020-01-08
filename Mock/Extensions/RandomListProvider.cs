using System.Collections.Generic;
using System.Linq;
using Mock.Generators;

namespace Mock.Extensions
{
    public class RandomListProvider<T> : List<T>
    {
        public List<int> Used = new List<int>();
    }
    public static class RandomListProviderExtension
    {
        public static T GetRandomUniqueValue<T>(this RandomListProvider<T> list)
        { 
            // hashcodes of list excluding used hashcodes
            var hashCodes = list.Select(x => x.GetHashCode()).Where(x => !list.Used.Contains(x)).ToList();

            // if all hashcodes is used
            if (hashCodes.Count == 0)
                return default;

            // get random hashcode in unused hashcodes 
            int hashResult = hashCodes[Utilities.Random.Next(hashCodes.Count)];

            // add used hashcode in used array 
            list.Used.Add(hashResult);

            // find element by hashcode 
            return list.Find(x => x.GetHashCode() == hashResult);
        }

        public static bool TryGetUniqueValue<T>(this RandomListProvider<T> list)
        {
            // // hashcodes of list excluding used hashcodes
            var hashCodes = list.Select(x => x.GetHashCode()).Where(x => !list.Used.Contains(x)).ToList();

            // if all hashcodes is used
            if (hashCodes.Count == 0)
                return false;

            // if we have unused hashcodes
            return true;
        }
    }
}
