using GitHooks.Commands;
using System.Linq;

namespace GitHooks
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            var context = new Context(args);
            var exitCode = Execute(context);
            return exitCode;
        }

        private static bool CheckPrerequisites()
        {
            if (!Bash.IsInstalled())
            {
                Output.WriteLine("fatal: bash installation not found");
                return false;
            }

            if (!Git.IsInstalled())
            {
                Output.WriteLine("fatal: git installation not found");
                return false;
            }

            if (!Git.IsRepository())
            {
                Output.WriteLine("fatal: not a git repository (or any of the parent directories)");
                return false;
            }

            return true;
        }

        private static int Execute(Context context)
        {
            if (!CheckPrerequisites())
                return 1;

            var commands = new ICommand[] { new Version(), new Help(), new Install(), new Uninstall(), new Run(), new List() };
            var command = commands.FirstOrDefault(c => c.IsMatch(context)) ?? new Unknown();
            var exitCode = command.Execute(context);
            return exitCode;
        }
    }
}
