using System;
using System.Collections.Generic;
using System.Linq;

namespace Mock.Utility
{
    public class ListExt<T> : List<T>
    {
        public HashSet<int> Used = new HashSet<int>();
    }
    public static class ListExtension
    {
        public static T GetRandomUniqueValue<T>(this List<T> list)
        {
            if (!(list is ListExt<T>))
                return default;

            var adapter = list as ListExt<T>;

            Random random = new Random();

            var hashCodes = list.Select(x => x.GetHashCode()).Where(x => !adapter.Used.Contains(x)).ToList();

            if (hashCodes.Count == 0)
                return default;

            int hashResult = hashCodes[random.Next(hashCodes.Count)];

            adapter.Used.Add(hashResult);

            return list.Find(x => x.GetHashCode() == hashResult);
        }
        public static T GetRandomValue<T>(this List<T> list)
        {
            Random random = new Random();
            int value = random.Next(0, list.Count);

            return list[value];
        }
    }
}
