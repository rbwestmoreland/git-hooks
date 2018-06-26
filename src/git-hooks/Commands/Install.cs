using System;
using System.IO;
using System.Text;

namespace GitHooks.Commands
{
    internal class Install : Command
    {
        public override bool IsMatch(Context context) => IsMatch(context, "install");

        public override int Execute(Context context)
        {
            if (!CheckPrerequisites())
                return 1;

            CreateGitDirectory();
            CreateRepositoryDirectory();
            CreateUserDirectory();

            Git.SetConfig("core.hooksPath", ".git/.githooks");

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
                    File.WriteAllText(file, $"#!/usr/bin/env bash{Environment.NewLine}git hooks run {hook} \"$@\"");
            }
        }

        private static void CreateRepositoryDirectory()
        {
            var path = Paths.Hooks.GetRepositoryPath();
            CreateDirectory(path);
        }

        private static void CreateUserDirectory()
        {
            var path = Paths.Hooks.GetUserProfilePath();
            CreateDirectory(path);
        }

        private static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var gitattributes = Path.Combine(path, ".gitattributes");
            if (!File.Exists(gitattributes))
                File.WriteAllText(gitattributes, "* text eol=lf", new UTF8Encoding(false));

            foreach (var hook in Git.GetHooks())
            {
                var subfolder = Path.Combine(path, hook);
                if (!Directory.Exists(subfolder))
                    Directory.CreateDirectory(subfolder);
            }
        }
    }
}
