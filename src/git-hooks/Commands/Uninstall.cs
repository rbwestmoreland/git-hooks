using System.IO;

namespace GitHooks.Commands
{
    internal class Uninstall : Command
    {
        public override bool IsMatch(Context context) => IsMatch(context, "uninstall");

        public override int Execute(Context context)
        {
            if (!CheckPrerequisites())
                return 1;

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
