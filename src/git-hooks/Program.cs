using System;
using System.Diagnostics;

namespace GitHooks
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            var context = new Context(args);
            var exitCode = Execute(context);

            if (Debugger.IsAttached)
                Debugger.Break();

            return exitCode;
        }

        private static int Execute(Context context)
        {
            switch (context.Command)
            {
                case "--version":
                    Console.WriteLine($"git-hooks {context.Version}");
                    return 0;
                default:
                    Console.WriteLine("unknown command");
                    return 1;
            }
        }
    }
}
