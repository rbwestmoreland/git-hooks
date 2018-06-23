using System.Collections.Generic;
using System.IO;

namespace GitHooks
{
    internal static class Paths
    {
        public enum Format
        {
            Absolute,
            Relative
        }

        public static class Environment
        {
            public static string GetUserProfilePath()
            {
                return System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile);
            }

            public static string GetGitRootPath()
            {
                return Git.GetRootPath();
            }
        }

        public static class Install
        {
            public static string GetRepositoryPath()
            {
                var gitRoot = Git.GetRootPath();
                var path = Path.Combine(gitRoot, ".git", "githooks");
                return path;
            }
        }

        public static class Hooks
        {
            public static string GetUserProfilePath()
            {
                var profile = Environment.GetUserProfilePath();
                var path = Path.Combine(profile, ".githooks");
                return path;
            }

            public static string GetRepositoryPath()
            {
                var gitRoot = Git.GetRootPath();
                var path = Path.Combine(gitRoot, ".githooks");
                return path;
            }

            public static IEnumerable<string> GetRepositoryFiles(string hook, Format format)
            {
                var gitRoot = Environment.GetGitRootPath();
                var directory = GetRepositoryPath();
                var subfolder = Path.Combine(directory, hook);

                if (!Directory.Exists(subfolder))
                    yield break;

                foreach (var file in Directory.EnumerateFiles(subfolder))
                {
                    yield return format == Format.Absolute ? file : file.Replace(gitRoot, ".");
                }
            }

            public static IEnumerable<string> GetUserProfileFiles(string hook, Format format)
            {
                var userProfile = Environment.GetUserProfilePath();
                var directory = GetUserProfilePath();
                var subfolder = Path.Combine(directory, hook);

                if (!Directory.Exists(subfolder))
                    yield break;

                foreach (var file in Directory.EnumerateFiles(subfolder))
                {
                    yield return format == Format.Absolute ? file : file.Replace(userProfile, "~");
                }
            }
        }
    }
}
