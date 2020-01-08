using System;

namespace Mock.Generators
{
    public static class Utilities
    {
        public static int CurrentUserId { get; set; }

        public static Random Random { get; set; } = new Random();
    }
}