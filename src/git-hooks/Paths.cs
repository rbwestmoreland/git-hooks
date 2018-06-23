using System.IO;

namespace GitHooks
{
    internal static class Paths
    {
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
        }
    }
}
