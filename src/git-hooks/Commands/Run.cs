using System;
using System.Linq;

namespace GitHooks.Commands
{
    internal class Run : ICommand
    {
        public bool IsMatch(Context context)
        {
            var command = context.Args.ElementAtOrDefault(0);
            return string.Equals("run", command, StringComparison.OrdinalIgnoreCase);
        }

        public int Execute(Context context)
        {
            var exit = 0;
            var hook = context.Args.ElementAtOrDefault(1);
            var args = context.Args.ElementAtOrDefault(2);

            Output.WriteLine("");

            foreach (var file in Paths.Hooks.GetRepositoryFiles(hook, Paths.Format.Absolute))
            {
                exit = Math.Max(exit, ExecuteHook(file, args));
            }

            foreach (var file in Paths.Hooks.GetUserProfileFiles(hook, Paths.Format.Absolute))
            {
                exit = Math.Max(exit, ExecuteHook(file, args));
            }

            return exit;
        }

        private static int ExecuteHook(string file, string args)
        {
            Output.WriteLine($"[{file}]");
            var exitCode = Bash.ExecuteScript(file, args, Output.WriteLine);
            Output.WriteLine("");
            return exitCode;
        }
    }
}
