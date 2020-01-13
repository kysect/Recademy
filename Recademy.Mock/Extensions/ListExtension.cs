using System.Collections.Generic;
using Mock.Generators;

namespace Mock.Extensions
{
    public static class ListExtension
    {
        public static T GetRandomValue<T>(this List<T> list)
        {
            int value = Utilities.Random.Next(0, list.Count);

            return list[value];
        }
    }
}