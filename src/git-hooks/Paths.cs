using System.Collections.Generic;
using System.IO;

namespace GitHooks
{
    internal static class Paths
    {
        private static string GetNormalizedPath(string value)
        {
            var path = Path.GetFullPath(value);
            return path;
        }

        public static class Environment
        {
            public static string GetUserProfilePath()
            {
                var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile);
                path = GetNormalizedPath(path);
                return path;
            }

            public static string GetGitRootPath()
            {
                var path = Git.GetRootPath();
                path = GetNormalizedPath(path);
                return path;
            }
        }

        public static class Install
        {
            public static string GetRepositoryPath()
            {
                var gitRoot = Git.GetRootPath();
                var path = Path.Combine(gitRoot, ".git", ".githooks");
                path = GetNormalizedPath(path);
                return path;
            }
        }

        public static class Hooks
        {
            public static string GetUserProfilePath()
            {
                var profile = Environment.GetUserProfilePath();
                var path = Path.Combine(profile, ".githooks");
                path = GetNormalizedPath(path);
                return path;
            }

            public static string GetRepositoryPath()
            {
                var gitRoot = Git.GetRootPath();
                var path = Path.Combine(gitRoot, ".githooks");
                path = GetNormalizedPath(path);
                return path;
            }

            public static IEnumerable<string> GetAllFiles(string hook)
            {
                foreach (var file in GetRepositoryFiles(hook))
                {
                    yield return file;
                }

                foreach (var file in GetUserProfileFiles(hook))
                {
                    yield return file;
                }
            }

            private static IEnumerable<string> GetRepositoryFiles(string hook)
            {
                var directory = GetRepositoryPath();
                var subfolder = Path.Combine(directory, hook);

                if (!Directory.Exists(subfolder))
                    yield break;

                foreach (var file in Directory.EnumerateFiles(subfolder))
                {
                    yield return GetNormalizedPath(file);
                }
            }

            private static IEnumerable<string> GetUserProfileFiles(string hook)
            {
                var directory = GetUserProfilePath();
                var subfolder = Path.Combine(directory, hook);

                if (!Directory.Exists(subfolder))
                    yield break;

                foreach (var file in Directory.EnumerateFiles(subfolder))
                {
                    yield return GetNormalizedPath(file);
                }
            }
        }
    }
}
