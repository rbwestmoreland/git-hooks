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

        private static int Execute(Context context)
        {
            var commands = new ICommand[] { new Version(), new Help(), new Install(), new Uninstall(), new Run(), new List() };
            var command = commands.FirstOrDefault(c => c.IsMatch(context)) ?? new Unknown();
            var exitCode = command.Execute(context);
            return exitCode;
        }
    }
}
