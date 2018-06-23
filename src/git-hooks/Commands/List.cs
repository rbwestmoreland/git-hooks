using System;
using System.Collections.Generic;
using System.IO;
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
                foreach (var file in GetRepositoryHooks(hook))
                {
                    Output.WriteLine($"[{hook}] {file}");
                }

                foreach (var file in GetUserHooks(hook))
                {
                    Output.WriteLine($"[{hook}] {file}");
                }
            }

            Output.WriteLine("");

            return 0;
        }

        private static IEnumerable<string> GetRepositoryHooks(string hook)
        {
            var gitRoot = Paths.Environment.GetGitRootPath();
            var directory = Paths.Hooks.GetRepositoryPath();
            var subfolder = Path.Combine(directory, hook);

            if (!Directory.Exists(subfolder))
                yield break;

            foreach (var file in Directory.EnumerateFiles(subfolder))
            {
                yield return file.Replace(gitRoot, ".");
            }
        }

        private static IEnumerable<string> GetUserHooks(string hook)
        {
            var userProfile = Paths.Environment.GetUserProfilePath();
            var directory = Paths.Hooks.GetUserProfilePath();
            var subfolder = Path.Combine(directory, hook);

            if (!Directory.Exists(subfolder))
                yield break;

            foreach (var file in Directory.EnumerateFiles(subfolder))
            {
                yield return file.Replace(userProfile, "~");
            }
        }
    }
}
