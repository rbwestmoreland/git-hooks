using System;
using System.IO;
using System.Linq;

namespace GitHooks.Commands
{
    internal class Uninstall : ICommand
    {
        public bool IsMatch(Context context)
        {
            var command = context.Args.ElementAtOrDefault(0);
            return string.Equals("uninstall", command, StringComparison.OrdinalIgnoreCase);
        }

        public int Execute(Context context)
        {
            if (!Git.IsRepository())
            {
                Output.WriteLine("fatal: not a git repository (or any of the parent directories)");
                return 1;
            }

            DeleteGitDirectory();
            Git.UnsetConfig("core.hooksPath");

            return 0;
        }

        private static void DeleteGitDirectory()
        {
            var directory = Paths.Install.GetRepositoryPath();
            if (!Directory.Exists(directory))
                Directory.Delete(directory);
        }
    }
}
