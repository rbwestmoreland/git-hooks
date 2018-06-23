using System;
using System.Linq;

namespace GitHooks.Commands
{
    internal class Help : ICommand
    {
        public bool IsMatch(Context context)
        {
            var option = context.Args.ElementAtOrDefault(0);
            var expected = new[] { "help", "--help", "?", "-?" };
            return expected.Any(v => string.Equals(v, option, StringComparison.OrdinalIgnoreCase));
        }

        public int Execute(Context context)
        {
            Output.WriteLine("usage: git hooks [--version] <command> [args]");
            Output.WriteLine("");
            Output.WriteLine("   --version        Display the installed version");
            Output.WriteLine("   help             Display all available commands");
            Output.WriteLine("   install          Install git hooks in the current repository");
            Output.WriteLine("   uninstall        Uninstall git hooks from the current repository");
            Output.WriteLine("   list             List installed git hooks");
            Output.WriteLine("   run              Run a specific git hook");
            Output.WriteLine("");
            return 0;
        }
    }
}
