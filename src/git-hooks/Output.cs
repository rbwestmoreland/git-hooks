using System;

namespace GitHooks
{
    internal static class Output
    {
        public static Action<string> WriteLine = Console.WriteLine;
    }
}
