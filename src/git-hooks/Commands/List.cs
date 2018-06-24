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
            Output.WriteLine("");

            foreach (var hook in Git.GetHooks())
            {
                foreach (var file in Paths.Hooks.GetAllFiles(hook))
                {
                    Output.WriteLine($"[{hook}] {file}");
                }
            }

            Output.WriteLine("");

            return 0;
        }


    }
}
