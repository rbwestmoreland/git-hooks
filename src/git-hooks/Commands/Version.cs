using System;
using System.Linq;

namespace GitHooks.Commands
{
    internal class Version : ICommand
    {
        public bool IsMatch(Context context)
        {
            var command = context.Args.ElementAtOrDefault(0);
            return string.Equals("--version", command, StringComparison.OrdinalIgnoreCase);
        }

        public int Execute(Context context)
        {
            Output.WriteLine($"git-hooks v{context.Version}");
            return 0;
        }
    }
}
