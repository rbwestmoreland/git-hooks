using System;
using System.IO;
using System.Linq;

namespace GitHooks.Commands
{
    internal class Install : ICommand
    {
        public bool IsMatch(Context context)
        {
            var command = context.Args.ElementAtOrDefault(0);
            return string.Equals("install", command, StringComparison.OrdinalIgnoreCase);
        }

        public int Execute(Context context)
        {
            if (!Git.IsRepository())
            {
                Output.WriteLine("fatal: not a git repository (or any of the parent directories)");
                return 1;
            }

            CreateGitDirectory();
            CreateRepositoryDirectory();
            CreateUserDirectory();

            Git.SetConfig("core.hooksPath", ".git/githooks");

            return 0;
        }

        private static void CreateGitDirectory()
        {
            var directory = Paths.Install.GetRepositoryPath();
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            foreach (var hook in Git.GetHooks())
            {
                var file = Path.Combine(directory, hook);
                if (!File.Exists(file))
                    File.WriteAllText(file, $"#!/usr/bin/env bash {Environment.NewLine} git hooks run \"{hook}\" \"$@\"");
            }
        }

        private static void CreateRepositoryDirectory()
        {
            var directory = Paths.Hooks.GetRepositoryPath();
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            foreach (var hook in Git.GetHooks())
            {
                var subfolder = Path.Combine(directory, hook);
                if (!Directory.Exists(subfolder))
                    Directory.CreateDirectory(subfolder);
            }
        }

        private static void CreateUserDirectory()
        {
            var directory = Paths.Hooks.GetUserProfilePath();
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            foreach (var hook in Git.GetHooks())
            {
                var subfolder = Path.Combine(directory, hook);
                if (!Directory.Exists(subfolder))
                    Directory.CreateDirectory(subfolder);
            }
        }
    }
}
