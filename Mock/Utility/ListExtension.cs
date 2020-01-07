using System;
using System.Collections.Generic;
using System.Text;

namespace Mock.Utility
{
    public static class ListExtension
    {
        public static T GetRandomValue<T>(this List<T> list)
        {
            Random random = new Random();
            int value = random.Next(0, list.Count);

            return list[value];
        }
    }
}
