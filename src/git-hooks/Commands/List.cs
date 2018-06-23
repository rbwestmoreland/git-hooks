using System;
using System.Linq;

namespace GitHooks.Commands
{
    internal class List : ICommand
    {
        public bool IsMatch(Context context)
        {
            var command = context.Args.ElementAtOrDefault(0);
            return string.Equals("list", command, StringComparison.OrdinalIgnoreCase);
        }

        public int Execute(Context context)
        {
            if (!Git.IsRepository())
            {
                Output.WriteLine("fatal: not a git repository (or any of the parent directories)");
                return 1;
            }

            Output.WriteLine("");

            foreach (var hook in Git.GetHooks())
            {
                foreach (var file in Paths.Hooks.GetRepositoryFiles(hook, Paths.Format.Relative))
                {
                    Output.WriteLine($"[{hook}] {file}");
                }

                foreach (var file in Paths.Hooks.GetUserProfileFiles(hook, Paths.Format.Relative))
                {
                    Output.WriteLine($"[{hook}] {file}");
                }
            }

            Output.WriteLine("");

            return 0;
        }


    }
}
